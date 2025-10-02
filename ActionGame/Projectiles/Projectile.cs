using System.Collections;
using PolyWare.Abilities;
using PolyWare.Characters;
using PolyWare.Core;
using PolyWare.Core.Entities;
using PolyWare.Effects;
using PolyWare.Levels;
using PolyWare.Utils;
using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : Entity<ProjectileData> {
		public Rigidbody MyRigidbody;
		
		public override ProjectileData Data { get; protected set; }
		
		private ProjectileMovementHandler movementHandler;
		private ContextHolder contextHolder;
		
		private void OnEnable() {
			Level.OnLevelReset += Kill;
		}
		
		private void OnDisable() {
			Level.OnLevelReset -= Kill;
		}

		private void Awake() {
			movementHandler = GetComponent<ProjectileMovementHandler>();
		}
		
		protected override void OnInitialize() {
			if (!MyRigidbody.isKinematic) {
				MyRigidbody.linearVelocity = MyRigidbody.transform.forward * Data.Speed;
			}
		}
		
		private void Start() {
			StartCoroutine(KillTimer());
		}

		private void FixedUpdate() {
			movementHandler.Move();
		}

		public void ProvideContext(ContextHolder ctxHolder) {
			contextHolder = ctxHolder;
		}
		
		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out IAffectable affectable)) {

				if (affectable.GameObject == Data.Invoker && !Data.Definition.AllowSelfDamage) return;

				if (affectable.GameObject.TryGetComponent(out IFactionMember factionMember) && factionMember.FactionID == Data.Faction) return; // todo: support friendlyFire setting (https://app.clickup.com/t/86b6wa8mj) 

				if (contextHolder.TryGet(out AbilityContext abilityContext)) {
					abilityContext.Ability.Execute(affectable, contextHolder);
				}
			}

			if (other.TryGetComponent(out Projectile otherProjectile) && otherProjectile.Data.Faction == Data.Faction) return;

			Kill();
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