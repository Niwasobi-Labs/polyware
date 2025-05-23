using UnityEngine;

namespace PolyWare.UI {
	public class Widget : MonoBehaviour {
		public virtual void Open() {
			gameObject.SetActive(true);
		}

		public virtual void Close() {
			if (gameObject) gameObject.SetActive(false);
		}
	}
}