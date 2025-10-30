using UnityEngine;

namespace PolyWare.Game {
	public class ProjectileData : EntityData<ProjectileDefinition> {
		public GameObject Invoker;
		public Transform Target;
		public float Speed;

		public ProjectileData(ProjectileDefinition definition) : base(definition) { }
		
		public ProjectileData(ProjectileDefinition definition, GameObject invoker, Transform target, float speed) : base(definition) {
			Invoker = invoker;
			Target = target;
			Speed = speed;
		}
	}
}