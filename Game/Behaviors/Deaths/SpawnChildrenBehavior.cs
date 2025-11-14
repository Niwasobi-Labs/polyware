using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class SpawnChildrenBehaviorFactory : IBehaviorFactory {
		[SerializeField] public EntityDefinition Prefab;
		[SerializeField] public int Count;
		[SerializeField] public float SpreadAngle;
		[SerializeField] public float Distance;
		[SerializeField] public float LaunchForce;
		
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
		
		public SpawnChildrenBehavior(ICharacter character, SpawnChildrenBehaviorFactory factory) : base(character) {
			prefab = factory.Prefab;
			count = factory.Count;
			spreadAngle = factory.SpreadAngle;
			distance = factory.Distance;
			launchForce = factory.LaunchForce;
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
				IEntity childSlime = EntityFactory<IEntity>.CreateFrom(prefab);
				Vector3 newRotation = Quaternion.AngleAxis(angle, parent.Transform.up) * -parent.Transform.forward;
				newRotation.Normalize();

				if (childSlime.GameObject.TryGetComponent(out Rigidbody rigidbody)) {
					Vector3 newPosition = parent.Transform.position + (newRotation * 1); // avoid full overlap
					childSlime.GameObject.transform.position = newPosition;
					rigidbody.AddForce(newRotation * launchForce, ForceMode.Impulse);
				}
				else {
					Vector3 newPosition = parent.Transform.position + (newRotation * distance);
					childSlime.GameObject.transform.position = newPosition;	
				}
			}
		}
	}
}