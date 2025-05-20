using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace PolyWare.UI {
	public class ActionRemapper : Widget {
		[Header("-- UI Fields --")] [SerializeField]
		private TMP_Text actionText;

		[SerializeField] private Button bindButton;
		[SerializeField] private TMP_Text bindButtonText;
		[SerializeField] private Button resetButton;

		[Header("-- Debugging --")] [SerializeField]
		private InputActionReference inputActionReference;

		[Range(0, 10)] [SerializeField] private int selectedBinding;

		[SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;
		private const bool excludeMouse = true;

		private string actionName;
		private int bindingIndex;

		private void Awake() {
			bindButton.onClick.AddListener(DoRebind);
			resetButton.onClick.AddListener(ResetBinding);
		}

		private void OnEnable() {
			if (inputActionReference) {
				Core.Input.LoadBindingOverride(actionName);
				GetBindingInfo();
				UpdateUI();
			}

			Core.Input.OnRebindComplete += UpdateUI;
			Core.Input.OnRebindCancel += UpdateUI;
			Core.Input.OnPlatformChange += UpdateUI;
		}

		private void OnDisable() {
			Core.Input.OnRebindComplete -= UpdateUI;
			Core.Input.OnRebindCancel -= UpdateUI;
			Core.Input.OnPlatformChange += UpdateUI;
		}

		private void OnValidate() {
			GetBindingInfo();
			UpdateUI();
		}

		private void GetBindingInfo() {
			actionName = inputActionReference.action.name;

			if (inputActionReference.action.bindings.Count > selectedBinding) bindingIndex = selectedBinding;
		}

		private void UpdateUI() {
			actionText.text = actionName;

			bindButtonText.text = Application.isPlaying ? Core.Input.GetBindingName(actionName, bindingIndex) : inputActionReference.action.GetBindingDisplayString(bindingIndex);
		}

		private void DoRebind() {
			Core.Input.StartRebind(actionName, bindingIndex, actionText, excludeMouse);
		}

		private void ResetBinding() {
			Core.Input.ResetBinding(actionName, bindingIndex);
			UpdateUI();
		}
	}
}