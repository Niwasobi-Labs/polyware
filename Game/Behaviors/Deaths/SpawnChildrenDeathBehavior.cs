using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class SpawnChildrenDeathBehaviorFactory : DeathBehaviorFactory {
		[SerializeField] public EntityDefinition Prefab;
		[SerializeField] public int Count;
		[SerializeField] public float SpreadAngle;
		[SerializeField] public float Distance;
		[SerializeField] public float LaunchForce;
		
		public override IBehavior Create(ICharacter parent) {
			return new SpawnChildrenDeathBehavior(parent, this);
		}
	}
	
	public class SpawnChildrenDeathBehavior : DeathBehavior {
		private readonly EntityDefinition prefab;
		private readonly int count;
		private readonly float spreadAngle;
		private readonly float distance;
		private readonly float launchForce;
		
		public SpawnChildrenDeathBehavior(ICharacter character, SpawnChildrenDeathBehaviorFactory factory) : base(character) {
			prefab = factory.Prefab;
			count = factory.Count;
			spreadAngle = factory.SpreadAngle;
			distance = factory.Distance;
			launchForce = factory.LaunchForce;
		}
		
		public override void Start() {
			Complete();
		}
		
		public override void Tick(float dt) {
			// no-op
		}
		
		public override void Complete() {
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