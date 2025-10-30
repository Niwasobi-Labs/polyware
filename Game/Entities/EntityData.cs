using PolyWare.Core;
using System;

namespace PolyWare.Game {

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