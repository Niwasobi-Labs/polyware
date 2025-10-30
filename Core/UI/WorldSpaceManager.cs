using UnityEngine;

namespace PolyWare.Core {
	public class WorldSpaceManager : MonoBehaviour {

		private Canvas canvas;
		
		public void RegisterWorldSpaceWidget(Widget widget) {
			widget.transform.SetParent(transform);
		}
	}
}