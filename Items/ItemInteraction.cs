using PolyWare.Characters;
using PolyWare.Interaction;
using UnityEngine;

namespace PolyWare.Items {
	public class ItemInteraction : Interactable {
		[SerializeField] protected Item item;

		protected override string GetPrompt() {
			return $"Hold X to pickup {item.Data.Name}";
		}

		public override void Interact(IProximityUser user) {
			base.Interact(user);

			if (user.GetUserObject().TryGetComponent(out ICharacter character)) character.Pickup(item);
		}
	}
}