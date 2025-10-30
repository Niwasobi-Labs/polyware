using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class TimedRadar : Radar {
		[SerializeField] private float tickRate;
		
		private CountdownTimer radarTimer;

		protected override void Awake() {
			base.Awake();
			
			radarTimer = new CountdownTimer(tickRate);
			radarTimer.OnTimerComplete += Scan;
		}

		private void OnEnable() {
			radarTimer.Start();
		}

		private void OnDisable() {
			radarTimer.Stop();
		}

		protected void Update() {
			radarTimer.Tick(Time.deltaTime);
		}

		protected override void Scan() {
			base.Scan();
			
			radarTimer.Restart();
		}
	}
}