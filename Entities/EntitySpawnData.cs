using System;

namespace PolyWare.Entities {
	
	public interface IEntitySpawnData {
		public EntityDefinition Definition { get; }
		public IEntityData OverrideData { get; }
	}
	
	

	[Serializable]
	public class EntitySpawnData<T, T2> : IEntitySpawnData where T : EntityDefinition where T2 : IEntityData {
		public T Definition;
		public T2 Overrides;
		
		EntityDefinition IEntitySpawnData.Definition => Definition;
		IEntityData IEntitySpawnData.OverrideData => Overrides;
	}
}