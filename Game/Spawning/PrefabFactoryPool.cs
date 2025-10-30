using UnityEngine;

namespace PolyWare.Game {
	public class PrefabFactoryPool : MonoBehaviour {
		public T SpawnPrefabAt<T>(GameObject prefabToSpawn, Vector3 position, Quaternion rotation) {
			GameObject newPrefab = Instantiate(prefabToSpawn, transform);
			newPrefab.transform.SetPositionAndRotation(position, rotation);
			return newPrefab.GetComponent<T>();
		}
	}
}