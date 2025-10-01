using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	public abstract class ProjectileMovementHandler : MonoBehaviour {
		protected Projectile projectile;

		public void Awake() {
			projectile = gameObject.GetComponent<Projectile>();
		}

		public abstract void Move();
	}
}