using PolyWare.Core.Events;
using UnityEngine;

namespace PolyWare.UI {
	public struct DigitalNavigationEvent : IEvent { }
	public struct CancelEvent : IEvent { }
	
	public struct MouseNavigationEvent : IEvent {
		public Vector2 MouseDelta;
		
		public MouseNavigationEvent(Vector2 mouseDelta) {
			MouseDelta = mouseDelta;
		}
	}
}