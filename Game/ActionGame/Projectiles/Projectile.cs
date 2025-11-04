using System.Collections;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : Entity<ProjectileData> {
		public Rigidbody MyRigidbody;
		
		public override ProjectileData Data { get; protected set; }
		
		private ProjectileMovementHandler movementHandler;
		private Ability ability;
		private AbilityContextHolder abilityCtxHolder;
		
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

		public void ProvideAbilityContext(AbilityContextHolder ctxHolder) {
			ability = ctxHolder.AbilityDefinition.CreateInstance();
			abilityCtxHolder = ctxHolder;
			abilityCtxHolder.Add(Data);
		}
		
		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out IAffectable affectable)) {
				if (affectable.GameObject.TryGetComponent(out IFactionMember otherFactionMember)) {
					if (!FactionSystem.CanDamageEachOther(Data.FactionContext, otherFactionMember.FactionInfo)) {
						if (FactionSystem.IsFriendly(Data.FactionContext, otherFactionMember.FactionInfo) && otherFactionMember.FactionInfo.AbsorbFriendlyHits) {
							Kill();
						}
						return;
					}
				}
				
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
			ServiceLocator.Global.Get<IAudioService>().PlayOneShot(Data.Definition.ImpactSound, transform.position);
			Destroy(gameObject);
		}
	}
}