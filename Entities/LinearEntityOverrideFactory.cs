using UnityEngine;

namespace PolyWare.Entities {
	public class LinearEntityOverrideFactory<T> : IEntityOverrideFactory<T> where T : IAllowSpawnOverride {
		private readonly EntityOverrideData[] data;
		private int index;
		
		public LinearEntityOverrideFactory(EntityOverrideData[] data) {
			this.data = data;
		}
		
		public T Create(Transform spawnPoint) {
			if (index >= data.Length) index = 0;
			return EntityOverrideFactoryHelper<T>.Create(spawnPoint, data[index++]);
		}
	}
}