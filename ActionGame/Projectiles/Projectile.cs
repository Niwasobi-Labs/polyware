using System.Collections;
using PolyWare.Abilities;
using PolyWare.Audio;
using PolyWare.Characters;
using PolyWare.Core;
using PolyWare.Core.Entities;
using PolyWare.Core.Services;
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
		private Ability ability;
		private AbilityContextHolder abilityCtxHolder;
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
			ability = ctxHolder.AbilityDefinition.CreateInstance();
			abilityCtxHolder = ctxHolder;
			abilityCtxHolder.Add(Data);
		}
		
		private void OnTriggerEnter(Collider other) {
			if (FactionMember != null && other.TryGetComponent(out Projectile otherProjectile) && FactionMember?.FactionID == otherProjectile.FactionMember?.FactionID) return;
			
			if (other.TryGetComponent(out IAffectable affectable)) {

				if (affectable.GameObject == Data.Invoker && !Data.Definition.AllowSelfDamage) return;

				if (FactionMember != null && affectable.GameObject.TryGetComponent(out IFactionMember otherFactionMember) && FactionMember?.FactionID == otherFactionMember.FactionID) return; // todo: support friendlyFire setting (https://app.clickup.com/t/86b6wa8mj) 

				if (ability != null) {
					abilityCtxHolder.Targets.Add(affectable.GameObject);
					ability.Trigger(abilityCtxHolder);
				}
			}
			
			Kill();
		}

		private IEnumerator KillTimer() {
			yield return Yielders.WaitForSeconds(10);
			Kill();
		}

		private void Kill() {
			ServiceLocator.Global.Get<IAudioService>().PlayRandomSfx(Data.Definition.ImpactSounds, transform.position, AudioChannel.Sfx);
			Destroy(gameObject);
		}
	}
}