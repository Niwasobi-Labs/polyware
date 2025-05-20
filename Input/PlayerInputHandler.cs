using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PolyWare.Input {
	[CreateAssetMenu(fileName = "PlayerInputHandler", menuName = "PolyWare/Input/PlayerInputHandler")]
	public class PlayerInputHandler : ScriptableObject, PolyWare_InputActions.IPlayerActions {
		public UnityAction Drop = delegate { };
		public UnityAction Interact = delegate { };
		public UnityAction Melee = delegate { };
		public UnityAction Reload = delegate { };
		public UnityAction Switch = delegate { };
		public UnityAction<bool> Use = delegate { };

		public Vector2 MoveDirection { get; private set; }
		public Vector2 LookDirection { get; private set; }

		public bool IsTryingToMove => MoveDirection.sqrMagnitude > 0;
		public bool IsTryingToLook => LookDirection.sqrMagnitude > 0;

		public bool IsActiveAimPressed { get; private set; }
		public bool IsTryingToJump { get; private set; }

		public void Initialize() {
			Core.Input.ActionMaps.Player.SetCallbacks(this);
		}
		
		public void OnActiveAim(InputAction.CallbackContext context) {
			IsActiveAimPressed = context.ReadValueAsButton();
		}

		public void OnDropEquipment(InputAction.CallbackContext context) {
			if (context.performed) Drop.Invoke();
		}

		public void OnInteract(InputAction.CallbackContext context) {
			if (context.performed) Interact.Invoke();
			if (context.canceled) Reload.Invoke();
		}

		public void OnJump(InputAction.CallbackContext context) {
			IsTryingToJump = context.ReadValueAsButton();
		}

		public void OnLook(InputAction.CallbackContext context) {
			LookDirection = context.ReadValue<Vector2>();
		}

		public void OnMelee(InputAction.CallbackContext context) {
			if (context.performed) Melee.Invoke();
		}

		public void OnMove(InputAction.CallbackContext context) {
			MoveDirection = context.ReadValue<Vector2>();
		}

		public void OnSwitchEquipment(InputAction.CallbackContext context) {
			if (context.performed) Switch.Invoke();
		}

		public void OnUse(InputAction.CallbackContext context) {
			Use.Invoke(context.ReadValueAsButton());
		}
	}
}