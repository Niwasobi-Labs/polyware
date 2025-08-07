using System;

namespace PolyWare.Entities {

	public interface IEntitySpawnData {
		public EntityDefinition Definition { get; }
		public ISpawnData Data { get; }
	}

	[Serializable]
	public class EntitySpawnData<T, T2> : IEntitySpawnData where T : EntityDefinition where T2 : ISpawnData {
		public T Definition;
		public T2 Data;
		EntityDefinition IEntitySpawnData.Definition => Definition;
		ISpawnData IEntitySpawnData.Data => Data;
	}
}