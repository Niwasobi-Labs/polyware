using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class SpawnChildrenBehaviorFactory : IBehaviorFactory {
		[SerializeField] public EntityDefinition Prefab;
		[SerializeField] public int Count;
		[SerializeField] public float SpreadAngle;
		[SerializeField] public float Distance;
		[SerializeField] public float LaunchForce;
		[SerializeField] public bool LaunchForward = true;
		[SerializeField] public bool KillChildrenOnDeath;
		
		public IBehavior Create(ICharacter parent) {
			return new SpawnChildrenBehavior(parent, this);
		}
	}
	
	public class SpawnChildrenBehavior : Behavior {
		private readonly EntityDefinition prefab;
		private readonly int count;
		private readonly float spreadAngle;
		private readonly float distance;
		private readonly float launchForce;
		private readonly bool launchForward;
		private readonly bool killChildrenOnDeath;
		
		private List<GameObject> spawnedChildren = new();
		
		public SpawnChildrenBehavior(ICharacter character, SpawnChildrenBehaviorFactory factory) : base(character) {
			prefab = factory.Prefab;
			count = factory.Count;
			spreadAngle = factory.SpreadAngle;
			distance = factory.Distance;
			launchForce = factory.LaunchForce;
			launchForward = factory.LaunchForward;
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
			float angleStep = spreadAngle / (count - 1);
			float startingAngle = -(spreadAngle / 2f);

			for (int i = 0; i < count; i++) {
				float angle = startingAngle + angleStep * i;
				IEntity child = EntityFactory<IEntity>.CreateFrom(prefab);
				// we take the reverse direction of our intention so that a 360 spread angle will launch the first bullet forward, or backwards if specified
				Vector3 newRotation = Quaternion.AngleAxis(angle, parent.Transform.up) * (launchForward ? -parent.Transform.forward : parent.Transform.forward);
				newRotation.Normalize();

				if (child.GameObject.TryGetComponent(out Rigidbody rigidbody)) {
					Vector3 newPosition = parent.Transform.position + (newRotation * 1); // avoid full overlap
					child.GameObject.transform.position = newPosition;
					rigidbody.AddForce(newRotation * launchForce, ForceMode.Impulse);
				}
				else {
					Vector3 newPosition = parent.Transform.position + (newRotation * distance);
					child.GameObject.transform.position = newPosition;	
				}

				if (killChildrenOnDeath) {
					spawnedChildren.Add(child.GameObject);
				}
			}
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