using PolyWare.Core.Entities;
using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	public class ProjectileData : EntityData<ProjectileDefinition> {
		public GameObject Invoker;
		public Transform Target;
		public Vector3 StartingDirection;
		public float Speed;
		public int Faction;
		
		public ProjectileData(ProjectileDefinition definition) : base(definition) { }
		
		public ProjectileData(ProjectileDefinition definition, GameObject invoker, Transform target, Vector3 startingDirection, float speed, int faction) : base(definition) {
			Invoker = invoker;
			Target = target;
			StartingDirection = startingDirection;
			Speed = speed;
			Faction = faction;
		}
	}
}