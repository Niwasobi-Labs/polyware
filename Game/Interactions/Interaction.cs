using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Interactions {
	public class Interaction : ProximityTarget, IInteractable {
		[Title("Interaction")]public bool DisablePrompt;

		[SerializeField] protected string prompt;

		public UnityEvent<IProximityUser> OnInteraction;
		
		private bool isPromptActive;

		public string InteractionPrompt {
			get {
				if (DisablePrompt) return string.Empty;
				return prompt != string.Empty ? prompt : $"Interact";
			}
		}

		public virtual void Interact(IProximityUser user) {
			OnInteraction.Invoke(user);
		}
	}
}