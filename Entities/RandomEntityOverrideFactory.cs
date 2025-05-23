using UnityEngine;

namespace PolyWare.Entities {
	public class RandomEntityOverrideFactory<T> : IEntityOverrideFactory<T> where T : IAllowSpawnOverride{
		private readonly EntityOverrideData[] data;
		
		public RandomEntityOverrideFactory(EntityOverrideData[] data) {
			this.data = data;
		}
		
		public T Create(Transform spawnPoint) {
			return EntityOverrideFactoryHelper<T>.Create(spawnPoint, data[Random.Range(0, data.Length)]);
		}
	}
}