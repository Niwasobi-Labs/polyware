using UnityEngine;

namespace PolyWare.Core {
	[RequireComponent(typeof(RectTransform))]
	public class Widget : MonoBehaviour {
		public virtual void Open() {
			gameObject.SetActive(true);
		}

		public virtual void Close() {
			if (gameObject) gameObject.SetActive(false);
		}
	}
}