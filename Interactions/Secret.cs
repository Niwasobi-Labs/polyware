using PolyWare.Debug;
using PolyWare.Characters;
using UnityEngine.Events;

namespace PolyWare.Interactions {
	public class Secret : Interaction {
		public readonly UnityAction OnFound = delegate { };

		protected override void OnProximityUserEnter(IProximityUser user) {
			if (!user.GetUserObject().TryGetComponent(out ICharacter character) || !character.IsPlayer) return;
			
			base.OnProximityUserEnter(user);
			
			Log.Message("Secret Found!");
			
			OnFound.Invoke();
		}
	}
}