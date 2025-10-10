using PolyWare.Core.Events;
using PolyWare.Core.Services;
using UnityEngine;

namespace PolyWare.UI {
	public class DigitalToMouseInputHandler : MonoBehaviour {
		public bool IsMouseActive { get; private set; }
		private EventBinding<DigitalNavigationEvent> digitalNavEventHandler;
		private EventBinding<MouseNavigationEvent> mouseNavEventHandler;
		
		private void OnEnable() {
			digitalNavEventHandler = new EventBinding<DigitalNavigationEvent>(ChangeToControllerNavigation);
			EventBus<DigitalNavigationEvent>.Subscribe(digitalNavEventHandler);
			
			mouseNavEventHandler = new EventBinding<MouseNavigationEvent>(ChangeToMouseNavigation);
			EventBus<MouseNavigationEvent>.Subscribe(mouseNavEventHandler);
		}
		
		private void OnDisable() {
			EventBus<DigitalNavigationEvent>.Unsubscribe(digitalNavEventHandler);
			EventBus<MouseNavigationEvent>.Unsubscribe(mouseNavEventHandler);
		}
		
		private void ChangeToMouseNavigation() {
			if (IsMouseActive) return;				
			
			ServiceLocator.Global.Get<IEventSystemService>().SetSelectedGameObject(null);
			IsMouseActive = true;
				
			Cursor.visible = true;
		}
		
		private void ChangeToControllerNavigation() {
			if (!IsMouseActive) return;
			
			IsMouseActive = false;
			Cursor.visible = false;
			
			ServiceLocator.Global.Get<IUIService>().GetTopScreen().Focus();
		}
	}
}