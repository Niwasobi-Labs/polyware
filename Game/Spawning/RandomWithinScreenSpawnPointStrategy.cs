using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class RandomWithinScreenSpawnPointStrategy : ISpawnPointStrategy {
		private readonly Vector3 axisNormal;

		public RandomWithinScreenSpawnPointStrategy(Vector3 axisNormal) {
			this.axisNormal = axisNormal;
		}
		
		public SpawnLocation NextSpawnPoint() {
			var location = new SpawnLocation {
				Position = Vector3.zero, 
				Rotation = Quaternion.identity
			};
			
			Vector3 worldPos = ServiceLocator.Global.Get<ICameraService>().ActiveCamera.ScreenToWorldPoint(new Vector3(Random.value, Random.value, 0));
			location.Position = Vector3.ProjectOnPlane(worldPos, axisNormal);
			
			return location;
		}
	}
}