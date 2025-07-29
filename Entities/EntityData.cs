using System;
using System.Reflection;
using UnityEngine;

namespace PolyWare.Entities {

	public interface IEntityData {
		public EntityDefinition EntityDefinition { get; }
	}
	
	[Serializable]
	public class EntityData<T> : IEntityData where T : EntityDefinition {
		public T Definition { get; }
		public EntityDefinition EntityDefinition => Definition;

		public EntityData(T definition) {
			Definition = definition;
		}
	}
}