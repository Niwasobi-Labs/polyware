using PolyWare.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PolyWare.Input {
	[CreateAssetMenu(fileName = "UIInputHandler", menuName = "PolyWare/Input/UIInputHandler")]
	public class UIInputHandler : ScriptableObject, PolyWare_InputActions.IUIActions {
		public UnityAction OnDigitalNavigation = delegate { };
		public UnityAction OnMouseNavigation = delegate { };
		
		public void Initialize() {
			Core.Input.ActionMaps.UI.SetCallbacks(this);
		}
		
		public void OnNavigate(InputAction.CallbackContext context) {
			OnDigitalNavigation.Invoke();
		}
		
		public void OnSubmit(InputAction.CallbackContext context) {
			// noop
		}
		
		public void OnCancel(InputAction.CallbackContext context) {
			// noop
		}
		
		public void OnPoint(InputAction.CallbackContext context) {
			// noop
		}
		
		public void OnLeftClick(InputAction.CallbackContext context) {
			// noop
		}
		
		public void OnMouseDelta(InputAction.CallbackContext context) {
			var mouseDelta = Core.Input.ActionMaps.UI.MouseDelta.ReadValue<Vector2>();
			
			if (mouseDelta.x > 0.01f || mouseDelta.y > 0.01f) OnMouseNavigation.Invoke();
		}
	}
}