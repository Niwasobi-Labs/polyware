using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace PolyWare.Input {
	public class InputManager : MonoBehaviour {
		public enum ActionMap {
			None,
			Player,
			UI
		}

		// platforms
		public enum Platform {
			None,
			Windows,
			Mac,
			Linux,
			PlayStation,
			Xbox,
			Nintendo
		}

		private const string rebindPromptText = "Press new binding";

		private const string keyboardControlScheme = "Keyboard&Mouse";
		private const string gamepadControlScheme = "Gamepad";

		private const string manufacturerNintendo = "Nintendo Co., Ltd.";
		private const string manufacturerSony = "Sony Interactive Entertainment";
		private const string manufacturerMicrosoft = "Microsoft";
		[FormerlySerializedAs("PlayerInput")] [SerializeField] private PlayerInput playerInput;

		[FormerlySerializedAs("CurrentPlatform")]
		[SerializeField] private Platform currentPlatform;

		[HideInInspector] public PolyWare_InputActions ActionMaps;

		// rebinding
		public event Action<InputAction, int> OnRebindStarted;
		public event Action OnRebindComplete;
		public event Action OnRebindCancel;
		public event Action OnPlatformChange;
		
		private void Awake() {
			ActionMaps = new PolyWare_InputActions();
		}

		public void Initialize() {
			DontDestroyOnLoad(gameObject);

			playerInput.onControlsChanged += OnControlsChanged;
			OnControlsChanged(playerInput);

			OnRebindStarted += (action, index) => Debug.Logger.Message($"Rebind of ({action} : {index}) Started...");
		}

		private void OnControlsChanged(PlayerInput input) {
			UpdatePlatform(input);
		}

		private void UpdatePlatform(PlayerInput input) {
			switch (input.currentControlScheme) {
				case gamepadControlScheme: {
					//	Note: After running tests with a Switch and PS controller hooked up 
					//	at the same time, they are put into different InputUsers so the first 
					//	device for each is the correct platform. This will need to be refactored 
					//	if local coop is ever needed 
					foreach (InputDevice device in input.devices) {
						if (device.description.manufacturer == manufacturerSony) {
							currentPlatform = Platform.PlayStation;
							break;
						}

						if (device.description.manufacturer == manufacturerNintendo) {
							currentPlatform = Platform.Nintendo;
							break;
						}

						if (device.description.manufacturer == manufacturerMicrosoft) {
							currentPlatform = Platform.Xbox;
							break;
						}

						Debug.Logger.Error("Unknown gamepad found, setting default gamepad platform");
						currentPlatform = Platform.Xbox;
					}

					break;
				}
				case keyboardControlScheme:
					currentPlatform = Application.platform switch {
						RuntimePlatform.OSXPlayer or RuntimePlatform.OSXEditor => Platform.Mac,
						RuntimePlatform.WindowsPlayer or RuntimePlatform.WindowsEditor => Platform.Windows,
						RuntimePlatform.LinuxPlayer or RuntimePlatform.LinuxEditor => Platform.Linux,
						_ => currentPlatform
					};

					break;
				default:
					Debug.Logger.Error($"Invalid Control Scheme: {input.currentControlScheme}");
					break;
			}

			OnPlatformChange?.Invoke();
		}

		public void ChangeToActionMap(ActionMap actionMap) {
			foreach (InputActionMap map in ActionMaps.asset.actionMaps) map.Disable();

			switch (actionMap) {
				case ActionMap.None:
					break;
				case ActionMap.Player:
					ActionMaps.Player.Enable();
					break;
				case ActionMap.UI:
					ActionMaps.UI.Enable();
					break;
				default:
					Debug.Logger.Error("Invalid Action Map provided");
					break;
			}
		}

		# region Control Rebinding
		public void StartRebind(string actionName, int bindingIndex, TMP_Text statusText, bool excludeMouse) {
			InputAction action = ActionMaps.asset.FindAction(actionName);

			if (action.bindings[bindingIndex].isComposite) {
				int firstPartIndex = bindingIndex + 1;
				if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite) DoRebind(action, bindingIndex, statusText, true, excludeMouse);
			}
			else {
				DoRebind(action, bindingIndex, statusText, false, excludeMouse);
			}
		}

		private void DoRebind(InputAction actionToRebind, int bindingIndex, TMP_Text statusText, bool allCompositeParts, bool excludeMouse) {
			statusText.text = rebindPromptText;

			actionToRebind.Disable();

			InputActionRebindingExtensions.RebindingOperation rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

			rebind.OnComplete(operation => {
				actionToRebind.Enable();
				operation.Dispose();

				if (allCompositeParts) {
					int nextBindingIndex = bindingIndex + 1;
					if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
						DoRebind(actionToRebind, nextBindingIndex, statusText, true, excludeMouse);
				}

				SaveBindingOverride(actionToRebind);
				OnRebindComplete?.Invoke();
			});

			rebind.OnCancel(operation => {
				actionToRebind.Enable();
				operation.Dispose();

				OnRebindCancel?.Invoke();
			});

			rebind.WithCancelingThrough("<Keyboard>/escape");

			if (excludeMouse)
				rebind.WithControlsExcluding("Mouse");

			OnRebindStarted?.Invoke(actionToRebind, bindingIndex);
			rebind.Start();
		}

		public string GetBindingName(string actionName, int bindingIndex) {
			InputAction action = ActionMaps.asset.FindAction(actionName);
			return action.GetBindingDisplayString(bindingIndex);
		}

		private static void SaveBindingOverride(InputAction action) {
			for (int i = 0; i < action.bindings.Count; i++) PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
		}

		public void LoadBindingOverride(string actionName) {
			InputAction action = ActionMaps.asset.FindAction(actionName);

			for (int i = 0; i < action.bindings.Count; i++)
				if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
					action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
		}

		public void ResetBinding(string actionName, int bindingIndex) {
			InputAction action = ActionMaps.asset.FindAction(actionName);

			if (action.bindings[bindingIndex].isComposite)
				for (int i = bindingIndex + 1; i < action.bindings.Count && action.bindings[i].isPartOfComposite; i++)
					action.RemoveBindingOverride(i);
			else
				action.RemoveBindingOverride(bindingIndex);

			SaveBindingOverride(action);
		}
		
		#endregion
	}
}