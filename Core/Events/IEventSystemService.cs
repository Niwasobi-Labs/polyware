using UnityEngine;

namespace PolyWare.Core.Services {
	public interface IEventSystemService : IService {
		public void SetSelectedGameObject(GameObject objectToSelect);
		public GameObject GetSelectedGameObject();
	}
}