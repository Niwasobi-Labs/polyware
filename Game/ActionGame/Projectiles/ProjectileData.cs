using UnityEngine;

namespace PolyWare.Game {
	public class ProjectileData : EntityData<ProjectileDefinition> {
		public FactionContext FactionContext;
		public Transform Target;
		public float Speed;

		public ProjectileData(ProjectileDefinition definition) : base(definition) { }
		
		public ProjectileData(ProjectileDefinition definition, FactionContext factionContext, Transform target, float speed) : base(definition) {
			FactionContext = factionContext;
			Target = target;
			Speed = speed;
		}
	}
}