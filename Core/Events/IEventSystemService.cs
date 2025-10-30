using UnityEngine;

namespace PolyWare.Core {
	public interface IEventSystemService : IService {
		public void SetSelectedGameObject(GameObject objectToSelect);
		public GameObject GetSelectedGameObject();
	}
}