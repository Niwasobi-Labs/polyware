using System;

namespace PolyWare.Entities {
	[Serializable]
	public abstract class SpawnData {
		public abstract EntityData EntityData { get; }
		public bool Override;
	}
}