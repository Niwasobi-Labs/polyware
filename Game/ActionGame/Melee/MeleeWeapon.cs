using System.Collections.Generic;
using PolyWare.Abilities;
using PolyWare.Characters;
using PolyWare.Combat;
using PolyWare.Effects;
using PolyWare.Timers;
using PolyWare.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.ActionGame {

	
	public class Melee : Weapon {
		private CountdownTimer cooldownTimer;
		private Collider[] meleeResults;
		private LayerMask meleeLayerMask;

		private const string damageLayerName = "Damage";

		[ShowInInspector, ReadOnly]
		public MeleeData MeleeData => Data as MeleeData;
		
		protected override void OnInitialize() {
			meleeLayerMask = LayerMask.GetMask(damageLayerName);
			cooldownTimer = new CountdownTimer(MeleeData.Definition.MeleeInfo.Cooldown);
			meleeResults = new Collider[4];
		}

		public override bool CanUse => !cooldownTimer.IsRunning;

		public override void Use() {
			int hits = Physics.OverlapSphereNonAlloc(transform.position, MeleeData.Definition.MeleeInfo.Range, meleeResults, meleeLayerMask);

			if (hits == 0) return;

			for (int i = 0; i < hits; i++) {
				if (!meleeResults[i] || !meleeResults[i].TryGetComponent(out IDamageable damageable) || !meleeResults[i].TryGetComponent(out IAffectable affectable)) continue;

				if (damageable.GameObject.TryGetComponent(out IFactionMember factionMember) && factionMember.FactionID == myCharacter.FactionMember.FactionID) continue; // todo: support friendlyFire setting (https://app.clickup.com/t/86b6wa8mj)
				
				Ability abilityInstance = MeleeData.MeleeDefinition.MeleeInfo.MeleeAbility.CreateInstance();
				abilityInstance.Trigger(new AbilityContextHolder(
					abilityInstance.Definition, 
					myCharacter.Transform.gameObject,
					new List<GameObject> { affectable.GameObject },
					new List<IContext> {
						MeleeData,
						new DamageContext(myCharacter.Transform.gameObject, MeleeData.MeleeDefinition.MeleeInfo.Damage, abilityInstance.Definition)
					})
				);
			}
			
			cooldownTimer.Start();
		}

		public override void StopUsing() {
			// noop
		}

		protected override void OnEquip() {
			// noop
		}

		protected override bool OnUnequip() {
			return true;
		}

		private void OnDrawGizmosSelected() {
			if (!Application.isPlaying) return;
			
			Gizmos.DrawSphere(transform.position, MeleeData.Definition.MeleeInfo.Range);
		}
	}
}