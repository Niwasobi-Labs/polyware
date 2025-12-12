using System;
using System.Collections.Generic;
using PolyWare.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public struct SpawnChildrenContext : IContext {
		public EntityDefinition Prefab;
		public int MinCount;
		public int MaxCount;
		public float SpreadAngle;
		public bool Launch;
		[ShowIf("Launch")] public float LaunchForce;
	}
	[Serializable]
	public class SpawnChildrenBehaviorFactory : BehaviorFactory {
		public SpawnChildrenContext SpawnSettings;
		public bool KillChildrenOnDeath;
		
		public override IBehavior Create(ICharacter parent) {
			return new SpawnChildrenBehavior(parent, this);
		}
	}
	
	public class SpawnChildrenBehavior : Behavior {
		private readonly SpawnChildrenContext spawnSettings;
		private readonly bool killChildrenOnDeath;
		
		private readonly List<GameObject> spawnedChildren = new();
		
		public SpawnChildrenBehavior(ICharacter character, SpawnChildrenBehaviorFactory factory) : base(character, factory) {
			spawnSettings = factory.SpawnSettings;
			killChildrenOnDeath = factory.KillChildrenOnDeath;

			if (killChildrenOnDeath && character.GameObject.TryGetComponent(out IDamageable damageable)) {
				damageable.OnDeath += KillAllChildren;
			}
		}
		
		protected override void OnStart() {
			Complete();
		}

		protected override void OnTick(float dt) {
			// no-op
		}

		protected override void OnComplete() {
			SpawnChildrenHelper.Spawn(parent.GameObject, spawnSettings, entity => {
				if (killChildrenOnDeath) {
					spawnedChildren.Add(entity.GameObject);
				}
			});
		}
		
		private void KillAllChildren(DamageContext dmgContext) {
			for (int i = 0; i < spawnedChildren.Count; i++) {
				if (spawnedChildren[i] == null) continue;
				if (spawnedChildren[i].TryGetComponent(out IDamageable damageable)) {
					damageable.Kill(dmgContext);		
				} else {
					UnityEngine.Object.Destroy(spawnedChildren[i]);
				}
			}
		}
	}
}