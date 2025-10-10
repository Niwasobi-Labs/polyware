using UnityEngine;
using UnityEngine.EventSystems;

namespace PolyWare.Core.Services {
	public class EventSystemService : MonoBehaviour, IEventSystemService {
		[SerializeField] private EventSystem eventSystem;

		private void Awake() {
			ServiceLocator.Global.Register<IEventSystemService>(this);
		}

		public void SetSelectedGameObject(GameObject objectToSelect) {
			eventSystem.SetSelectedGameObject(objectToSelect);
		}

		public GameObject GetSelectedGameObject() {
			return eventSystem.currentSelectedGameObject;
		}
	}
}