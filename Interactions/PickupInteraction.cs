using PolyWare.Characters;
using PolyWare.Debug;
using PolyWare.Items;
using UnityEngine;

namespace PolyWare.Interactions {
	public class PickupInteraction : Interaction {
		[SerializeField] protected GameObject pickup;
		
		private void OnValidate() {
			if (!pickup.TryGetComponent(out IPickupable _)) {
				Log.Error("PickupInteraction requires a component that implements IPickupable", this);
			}
		}

		// protected override string GetPrompt() {
		// 	return $"Hold X to pickup {item.Data.Name}";
		// }

		public override void Interact(IProximityUser user) {
			base.Interact(user);

			if (user.GetUserObject().TryGetComponent(out ICharacter character)) character.Pickup(pickup.GetComponent<IPickupable>());
		}
	}
}