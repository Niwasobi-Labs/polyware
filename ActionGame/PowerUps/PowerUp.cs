using PolyWare.Characters;
using PolyWare.Debug;
using PolyWare.Core.Entities;
using PolyWare.Interactions;
using PolyWare.Items;

namespace PolyWare.ActionGame.PowerUps {
	public abstract class PowerUp : Entity<EntityData<PowerUpDefinition>>, IPickupable {
		protected ICharacter character;

		// todo: remove the user from Pickup and handle powerups through this auto-prop
		public bool AutoPickup => true;

		public void Pickup(IProximityUser user) {
			if (!user.GetUserObject().TryGetComponent(out character)) {
				Log.Error($"Can't pickup { name } with {user.GetUserObject().name}");
				return;
			}
			
			// PolyWare.Core.Instance.SfxManager.PlayClip(Data.PickupSound, transform);
			OnUse();
		}

		public abstract void OnUse();
	}
}