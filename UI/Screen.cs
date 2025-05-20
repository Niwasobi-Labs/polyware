using PolyWare.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PolyWare.UI {
	public class Screen : MonoBehaviour {
		[Header("Screen")] [SerializeField] private bool persistant = true;

		[SerializeField] private bool focusOnOpen = true;
		[SerializeField] private bool rememberFocusOnClose;
		[SerializeField] private GameObject defaultSelectedObject;

		private GameObject lastSelectedObject;

		public bool IsOpen { get; private set; }
		public bool Persistant => persistant;

		private void EnableInputEvents() {
			Core.Input.ActionMaps.UI.Cancel.performed += OnCancel;
		}

		private void DisableInputEvents() {
			Core.Input.ActionMaps.UI.Cancel.performed -= OnCancel;
		}

		protected virtual void OnCancel(InputAction.CallbackContext context) {
			Close();
		}

		public virtual void Open() {
			IsOpen = true;
			gameObject.SetActive(true);
				
			if (!focusOnOpen) return;
				
			EnableInputEvents();
			Core.Input.ChangeToActionMap(InputManager.ActionMap.UI);
			Focus();
		}

		public virtual void Close() {
			if (rememberFocusOnClose) RememberCurrentlySelectedObject();

			IsOpen = false;
			DisableInputEvents();
			gameObject.SetActive(false);
		}

		public void Focus() {
			if (Core.UI.IsMouseActive) return;

			if (lastSelectedObject) {
				Core.EventSystem.SetSelectedGameObject(lastSelectedObject.TryGetComponent(out IFocusable widget) ? widget.GetFocusObject() : lastSelectedObject.gameObject);
				lastSelectedObject = null;
			}
			else if (defaultSelectedObject) {
				Core.EventSystem.SetSelectedGameObject(defaultSelectedObject.TryGetComponent(out IFocusable widget) ? widget.GetFocusObject() : defaultSelectedObject);
			}
		}

		private void RememberCurrentlySelectedObject() {
			lastSelectedObject = Core.EventSystem.currentSelectedGameObject;
		}
	}
}