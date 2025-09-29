using System;
using UnityEngine;

namespace PolyWare.Combat {
	[Serializable]
	public struct DamageInfo {
		public int FactionID;
		public float Damage;
		public Element Element;
		public bool PenetrateShields;

		public DamageInfo(int factionID, DamageInfo damageInfo) : this(factionID, damageInfo.Element, damageInfo.Damage, damageInfo.PenetrateShields) { }

		public DamageInfo(int factionID, Element element, float damage, bool penetrateShields) {
			FactionID = factionID;
			Element = element;
			Damage = damage;
			PenetrateShields = penetrateShields;
		}
	}
}