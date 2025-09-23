using PolyWare.Debug;
using UnityEngine;

namespace PolyWare.Core.Spawning {
	public class RandomRadiusAlongAxisSpawnPointStrategy : ISpawnPointStrategy {
		
		private readonly Vector3 center;
		private readonly Vector3 axisNormal;
		private readonly float innerRadius;
		private readonly float outerRadius;
		
		public RandomRadiusAlongAxisSpawnPointStrategy(Vector3 center, float innerRadius, float outerRadius, Vector3 axisNormal) {
			this.center = center;
			this.innerRadius = innerRadius;
			this.outerRadius = outerRadius;
			this.axisNormal = axisNormal;
		}
		
		public SpawnLocation NextSpawnPoint() {
			var randomPoint = center + UnityEngine.Random.insideUnitSphere * outerRadius + Vector3.one * innerRadius;
			return new SpawnLocation(Vector3.ProjectOnPlane(randomPoint, axisNormal), Quaternion.identity);
		}
	}
}