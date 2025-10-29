using UnityEngine;

namespace PolyWare.UI {
	public class WorldSpaceManager : MonoBehaviour {

		private Canvas canvas;
		
		public void RegisterWorldSpaceWidget(Widget widget) {
			widget.transform.SetParent(transform);
		}
	}
}