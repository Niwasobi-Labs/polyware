using System;
using UnityEngine;
using Random = System.Random;

namespace PolyWare.Game {
	public static class SpawnChildrenHelper {
		public static void Spawn(GameObject parent, SpawnChildrenContext context, Action<IEntity> onChildSpawned) {
			int spawnCount = UnityEngine.Random.Range(context.MinCount, context.MaxCount);
			float angleStep = context.SpreadAngle / (spawnCount - 1);
			float startingAngle = -(context.SpreadAngle / 2f);

			for (int i = 0; i < spawnCount; i++) {
				float angle = startingAngle + angleStep * i;
				IEntity child = EntityFactory<IEntity>.CreateFrom(context.Prefab);
				// we take the reverse direction of our intention so that a 360 spread angle will launch the first bullet forward, or backwards if specified
				Vector3 newRotation = Quaternion.AngleAxis(angle, parent.transform.up) * -parent.transform.forward;
				newRotation.Normalize();

				if (child.GameObject.TryGetComponent(out Rigidbody rigidbody)) {
					Vector3 newPosition = parent.transform.position + (newRotation * 1); // avoid full overlap
					child.GameObject.transform.position = newPosition;
					rigidbody.AddForce(newRotation * context.LaunchForce, ForceMode.Impulse);
				}
				else {
					Vector3 newPosition = parent.transform.position + (newRotation * context.LaunchForce);
					child.GameObject.transform.position = newPosition;	
				}

				onChildSpawned?.Invoke(child);
			}
		}
	}
}