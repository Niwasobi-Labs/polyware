using System;
using PolyWare.Core.Entities;
using PolyWare.Core.Spawning;
using UnityEngine;

namespace PolyWare.ActionGame.Guns {
	public class GunSpawner : SpawnManager {

		[Serializable]
		public class GunSpawnerData : EntitySpawnerData<GunDefinition, GunSpawnData> { }
		
		[SerializeField] private GunSpawnerData[] guns;
		
		private EntitySpawner<Gun> spawner;

		protected override void Awake() {
			base.Awake();
			spawner =  new EntitySpawner<Gun>(random ? new RandomEntitySpawner<Gun, GunDefinition, GunSpawnData>(guns) : new LinearEntitySpawner<Gun, GunDefinition, GunSpawnData>(guns), spawnPointStrategy);
		}
		
		public override void Spawn() {
			for (int i = 0; i < spawnCount; i++) {
				spawner.Spawn();
			}
		}
	}
}