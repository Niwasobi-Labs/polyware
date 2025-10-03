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
		private AbilityContextHolder abilityContextHolder;
		public IFactionMember FactionMember { get; private set; }
		
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
			
			if (Data.Invoker) FactionMember = Data.Invoker.GetComponent<IFactionMember>();
		}
		
		private void Start() {
			StartCoroutine(KillTimer());
		}

		private void FixedUpdate() {
			movementHandler.Move();
		}

		public void ProvideAbilityContext(AbilityContextHolder ctxHolder) {
			abilityContextHolder = ctxHolder;
		}
		
		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out IAffectable affectable)) {

				if (affectable.GameObject == Data.Invoker && !Data.Definition.AllowSelfDamage) return;

				if (FactionMember != null && affectable.GameObject.TryGetComponent(out IFactionMember otherFactionMember) && FactionMember?.FactionID == otherFactionMember.FactionID) return; // todo: support friendlyFire setting (https://app.clickup.com/t/86b6wa8mj) 

				if (abilityContextHolder != null) {
					abilityContextHolder.AbilityContext.Targets.Add(affectable.GameObject);
					abilityContextHolder.AbilityContext.Ability.Trigger(abilityContextHolder);
				}
			}

			if (FactionMember != null && other.TryGetComponent(out Projectile otherProjectile) && FactionMember?.FactionID == otherProjectile.FactionMember?.FactionID) return;

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