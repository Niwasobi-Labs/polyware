using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class Grenade : Entity<GrenadeData>, IPickupable, IThrowable {
		public override GrenadeData Data { get; protected set; }
		
		private CountdownTimer fuseTimer;

		protected override void OnInitialize() {
			fuseTimer = new CountdownTimer(Data.Definition.FuseTime);
			fuseTimer.OnTimerComplete += Explode;
		}

		protected virtual void Update() {
			fuseTimer.Tick(Time.deltaTime);
		}

		public bool AutoPickup => true;

		public void Pickup(IProximityUser user) {
			Destroy(gameObject);
		}

		public virtual void PrepareThrow() {
			Log.Message("Preparing to throw . . .");
		}
		
		public virtual void Throw() {
			Log.Message("Throwing!");
			fuseTimer.Start();
		}

		protected virtual void Explode() {
			Log.Message("BOOM!");
			Destroy(gameObject);
		}
	}
}