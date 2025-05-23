using System;

namespace PolyWare.Entities {
	[Serializable]
	public abstract class EntityOverrideData {
		public abstract EntityData EntityData { get; }
		public bool Override;
	}
}