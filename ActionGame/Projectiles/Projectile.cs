using System.Collections;
using PolyWare.Combat;
using PolyWare.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.ActionGame.Projectiles {
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour {
		[FormerlySerializedAs("ProjectileData")] [SerializeField] protected ProjectileData projectileData; 
		private DamageInfo damage;
		private GameObject invoker;

		private Rigidbody rigidbdy;

		protected float speed;
		protected Transform target;

		private void Awake() {
			rigidbdy = GetComponent<Rigidbody>();
		}

		private void Start() {
			if (!rigidbdy.isKinematic) rigidbdy.linearVelocity = rigidbdy.transform.forward * speed;

			StartCoroutine(KillTimer());
		}

		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out IDamageable damageable)) {
				damageable.TakeDamage(damage);

				// if (damage.FromPlayer) PlayerCharacter.OnPlayerLandedShot?.Invoke(); bug: this shouldn't be handled this way, use static event
			}

			Kill();
		}

		public void Initialize(DamageInfo damageInfo, float velocity, Vector3 direction) {
			transform.forward = direction;
			damage = damageInfo;
			speed = velocity;
			target = null;
		}
		
		public void Initialize(DamageInfo damageInfo, float velocity, Vector3 direction, Transform newTarget) {
			transform.forward = direction;
			target = newTarget;
			damage = damageInfo;
			speed = velocity;
		}

		private IEnumerator KillTimer() {
			yield return Yielders.WaitForSeconds(10);
			Kill();
		}

		private void Kill() {
			Destroy(gameObject);
		}
	}
}