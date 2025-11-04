using UnityEngine;

namespace PolyWare.Game {
	public static class FactionSystem {
		public static bool CanDamageEachOther(FactionContext culprit, FactionContext victim) {
			return !IsFriendly(culprit, victim) || victim.AllowFriendlyFire;
		}
		
		public static bool CanDamageEachOther(FactionContext culprit, GameObject victim) {
			if (!victim) return true;
			
			return !victim.TryGetComponent(out IFactionMember victimFaction) || CanDamageEachOther(culprit, victimFaction.FactionInfo);
		}

		public static bool IsFriendly(FactionContext culprit, FactionContext victim) {
			return culprit.ID == victim.ID; // todo: support alliances later
		}
	}
}