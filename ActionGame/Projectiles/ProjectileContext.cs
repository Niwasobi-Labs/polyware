using PolyWare.Core;
using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	// is this just ProjectileData?
	public class ProjectileContext : IContext {
		public readonly ProjectileData Data;
		public int SpawnCount;
		public float Spread;
		public Vector3 SpawnUp;
		
		public ProjectileContext(ProjectileData data, int spawnCount, float spread, Vector3 spawnUp) {
			Data = data;
			SpawnCount = spawnCount;
			Spread = spread;
			SpawnUp = spawnUp;
		}
	}
}