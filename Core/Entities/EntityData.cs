using System;
using PolyWare.Utils;

namespace PolyWare.Core.Entities {

	public interface IEntityData {
		public EntityDefinition EntityDefinition { get; }
	}
	
	[Serializable]
	public class EntityData<T> : IEntityData, IContext where T : EntityDefinition {
		public T Definition { get; }
		public EntityDefinition EntityDefinition => Definition;

		public EntityData(T definition) {
			Definition = definition;
		}
	}
}