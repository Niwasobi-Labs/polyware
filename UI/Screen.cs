using PolyWare.Core;
using PolyWare.Core.Events;
using UnityEngine;

namespace PolyWare.UI {
	public class Screen : MonoBehaviour {
		[Header("Screen")] [SerializeField] private bool persistant = true;

		[SerializeField] private bool focusOnOpen = true;
		[SerializeField] private bool rememberFocusOnClose;
		[SerializeField] private GameObject defaultSelectedObject;

		private GameObject lastSelectedObject;

		public bool IsOpen { get; private set; }
		public bool Persistant => persistant;

		private EventBinding<CancelEvent> cancelEventHandler;

		private void EnableInputEvents() {
			cancelEventHandler = new EventBinding<CancelEvent>(OnCancel);
			EventBus<CancelEvent>.Subscribe(cancelEventHandler);
		}

		private void DisableInputEvents() {
			EventBus<CancelEvent>.Unsubscribe(cancelEventHandler);
		}

		protected virtual void OnCancel() {
			Close();
		}

		// task: consider switching this to call an OnOpen that children will override instead of this
		public virtual bool Open() {
			IsOpen = true;
			gameObject.SetActive(true);
				
			if (!focusOnOpen) return false;
				
			EnableInputEvents();
			// Instance.Input.ChangeToActionMap(InputManager.ActionMap.UI); // todo: send event to game that input needs to be changed to UI?
			Focus();
			return true;
		}

		public virtual void Close() {
			if (rememberFocusOnClose) RememberCurrentlySelectedObject();

			IsOpen = false;
			DisableInputEvents();
			gameObject.SetActive(false);
		}

		public void Focus() {
			if (Instance.UI.IsMouseActive) return;

			if (lastSelectedObject) {
				Instance.EventSystem.SetSelectedGameObject(lastSelectedObject.TryGetComponent(out IFocusable widget) ? widget.GetFocusObject() : lastSelectedObject.gameObject);
				lastSelectedObject = null;
			}
			else if (defaultSelectedObject) {
				Instance.EventSystem.SetSelectedGameObject(defaultSelectedObject.TryGetComponent(out IFocusable widget) ? widget.GetFocusObject() : defaultSelectedObject);
			}
		}

		private void RememberCurrentlySelectedObject() {
			lastSelectedObject = Instance.EventSystem.currentSelectedGameObject;
		}
	}
}