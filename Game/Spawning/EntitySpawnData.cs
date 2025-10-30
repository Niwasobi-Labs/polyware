using System;

namespace PolyWare.Game {

	public interface IEntitySpawnerData {
		public EntityDefinition Definition { get; }
		public IEntitySpawnData Data { get; }
	}

	[Serializable]
	public class EntitySpawnerData<T, T2> : IEntitySpawnerData where T : EntityDefinition where T2 : IEntitySpawnData {
		public T Definition;
		public T2 SpawnData;
		EntityDefinition IEntitySpawnerData.Definition => Definition;
		IEntitySpawnData IEntitySpawnerData.Data => SpawnData;
	}
}