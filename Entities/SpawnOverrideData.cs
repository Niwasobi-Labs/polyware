using System;

namespace PolyWare.Entities {
	[Serializable]
	public abstract class SpawnOverrideData {
		public abstract EntityData EntityData { get; }
		public bool Override;
	}
}