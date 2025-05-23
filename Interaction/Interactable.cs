using System;
using PolyWare.Characters;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Interaction {
	public class Interactable : ProximityDetector, IInteractable {
		[Title("Interaction")] [SerializeField] protected bool usePrompt;

		[SerializeField] protected string prompt;

		public UnityEvent OnInteraction;
		
		public static Action<string> OnPlayerEntered = delegate { };
		public static Action OnPlayerExited = delegate { };

		private bool isPromptActive;

		private void OnDisable() {
			if (isPromptActive) HidePrompt();
		}

		public virtual void Interact(IProximityUser user) {
			OnInteraction.Invoke();
		}

		protected virtual string GetPrompt() {
			return prompt != string.Empty ? prompt : $"Interact {GetGameObject().name}";
		}

		protected override void OnProximityUserEnter(IProximityUser user) {
			base.OnProximityUserEnter(user);

			if (user.GetUserObject().TryGetComponent(out ICharacter character) && character.IsPlayer) ShowPrompt();
		}

		protected override void OnProximityUserExit(IProximityUser user) {
			base.OnProximityUserExit(user);

			if (user.GetUserObject().TryGetComponent(out ICharacter character) && character.IsPlayer) HidePrompt();
		}

		private void ShowPrompt() {
			OnPlayerEntered.Invoke(GetPrompt());
			isPromptActive = true;
		}

		private void HidePrompt() {
			OnPlayerExited.Invoke();
			isPromptActive = false;
		}
	}
}