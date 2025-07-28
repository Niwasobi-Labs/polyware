using System;

namespace PolyWare.Entities {
	
	public interface IEntitySpawnData {
		public EntityDefinition Definition { get; }
		public IEntityOverrideData OverrideData { get; }
		public bool Override { get; }
	}
	
	[Serializable]
	public class EntitySpawnData<T, T2> : IEntitySpawnData where T : EntityDefinition where T2 : IEntityOverrideData {
		public T Definition;
		public bool Override;
		public T2 Overrides;
		EntityDefinition IEntitySpawnData.Definition => Definition;
		bool IEntitySpawnData.Override => Override;
		IEntityOverrideData IEntitySpawnData.OverrideData => Overrides;
	}
}