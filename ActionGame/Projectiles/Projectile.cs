using System.Collections;
using PolyWare.Characters;
using PolyWare.Combat;
using PolyWare.Levels;
using PolyWare.Utils;
using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour {
		[SerializeField] protected ProjectileDefinition projectileDefinition; 
		private DamageInfo damage;
		private GameObject invoker;

		private Rigidbody rigidbdy;

		protected float speed;
		protected Transform target;

		private void OnEnable() {
			Level.OnLevelReset += Kill;
		}
		
		private void OnDisable() {
			Level.OnLevelReset -= Kill;
		}
		
		private void Awake() {
			rigidbdy = GetComponent<Rigidbody>();
		}
		
		private void Start() {
			if (!rigidbdy.isKinematic) rigidbdy.linearVelocity = rigidbdy.transform.forward * speed;

			StartCoroutine(KillTimer());
		}

		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out IDamageable damageable)) {

				if (damageable.GameObject == invoker && !projectileDefinition.AllowSelfDamage) return;

				if (damageable.GameObject.TryGetComponent(out IFactionMember factionMember) && factionMember.FactionID == damage.FactionID) return; // todo: support friendlyFire setting (https://app.clickup.com/t/86b6wa8mj) 
				
				damageable.TakeDamage(damage);

				// if (damage.FromPlayer) PlayerCharacter.OnPlayerLandedShot?.Invoke(); bug: this shouldn't be handled this way, use static event
			}

			Kill();
		}

		public void Initialize(GameObject invokedBy, DamageInfo damageInfo, float velocity, Vector3 direction, Transform newTarget = null) {
			invoker = invokedBy;
			transform.forward = direction;
			damage = damageInfo;
			speed = velocity;
			target = newTarget;
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