using UnityEngine;

namespace PolyWare.Game {
	public abstract class ProjectileMovementHandler : MonoBehaviour {
		protected Projectile projectile;

		public void Awake() {
			projectile = gameObject.GetComponent<Projectile>();
		}

		public abstract void Move();
	}
}