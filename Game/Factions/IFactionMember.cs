using UnityEngine;

namespace PolyWare.Game {
	public interface IFactionMember {
		int FactionID { get; set; }
		FactionMemberData FactionInfo { get; set; }

		public bool CanDamage(IFactionMember other) {
			return other.FactionID != FactionID || other.FactionInfo.AllowFriendlyFire;
		}

		public static bool CanDamageEachOther(GameObject culprit, GameObject victim) {
			if (culprit.TryGetComponent(out IFactionMember culpritFaction) && victim.TryGetComponent(out IFactionMember victimFaction)) {
				return culpritFaction.FactionID != victimFaction.FactionID || victimFaction.FactionInfo.AllowFriendlyFire;
			}

			return false;
		}
	}
}