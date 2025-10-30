using System;
using UnityEngine;

namespace PolyWare.Game {
	public abstract class Radar : MonoBehaviour {
		[SerializeField] private IRadarStrategy.RadarStrategyType type;
		[SerializeField] private RadarSettings settings;
		protected RadarData data;
		
		protected IRadarStrategy radarStrategy;
		
		public event Action<RadarData> OnRadarUpdate;

		protected virtual void Awake() {
			data = new RadarData(settings);
			
			radarStrategy = IRadarStrategy.Create(type, settings);
		}

		protected virtual void Scan() {
			data.Clear();

			radarStrategy.Scan(transform, data.Local, settings.LocalRange, settings.LayerMask);
			SortData(data.Local, settings.LocalSortingMethod);
			
			if (ShouldSearchForDistantObjects()) {
				radarStrategy.Scan(transform, data.Distant, settings.DistantRange, settings.LayerMask);
				SortData(data.Distant, settings.DistantSortingMethod);
			}
			
			RaiseRadarUpdate();
		}
		
		public void SortData(RadarData.ScanData scanData, RadarSettings.SortType sortingMethod) {
			switch (sortingMethod) {
				case RadarSettings.SortType.None:
					break;
				case RadarSettings.SortType.ClosestFirst:
					scanData.Results.Sort((targetA, targetB) => {
						float distA = Vector3.Distance(transform.position, targetA.GameObject.transform.position);
						float distB = Vector3.Distance(transform.position, targetB.GameObject.transform.position);
						return distA.CompareTo(distB);
					});
					break;
				case RadarSettings.SortType.FarthestFirst:
					scanData.Results.Sort((targetA, targetB) => {
						float distA = Vector3.Distance(transform.position, targetA.GameObject.transform.position);
						float distB = Vector3.Distance(transform.position, targetB.GameObject.transform.position);
						return distB.CompareTo(distA);
					});
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private bool ShouldSearchForDistantObjects() {
			return settings.DistantUpdateMethod switch {
				RadarSettings.DistantUpdateType.Never => false,
				RadarSettings.DistantUpdateType.Always => true,
				RadarSettings.DistantUpdateType.WhenNoLocalsAreNotFound => data.Local.Count == 0,
				_ => throw new ArgumentOutOfRangeException()
			};
		}
		
		protected void RaiseRadarUpdate() {
			OnRadarUpdate?.Invoke(data);
		}
		
		private void OnDrawGizmosSelected() {
			if (!Application.isPlaying) return;
			
			Gizmos.color = Color.red;
			radarStrategy.DrawGizmos(transform, settings.LocalRange);
			
			Gizmos.color = Color.yellow;
			radarStrategy.DrawGizmos(transform, settings.DistantRange);

			if (data.Local.Count > 0) {
				Gizmos.color = Color.purple;
				Gizmos.DrawWireSphere(data.Local.Results[0].GameObject.transform.position, 3);	
			}
			
			if (data.Distant.Count > 0) {
				Gizmos.color = Color.cyan;
				Gizmos.DrawWireSphere(data.Distant.Results[0].GameObject.transform.position, 3);	
			}
		}
	}
}