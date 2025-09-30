using System;
using UnityEngine;

namespace PolyWare.Combat {
	[Serializable]
	public struct DamageInfo {
		[HideInInspector] public int FactionID;
		public float Damage;
		public Element Element;
		public bool PenetrateShields;
		public float Knockback;

		public DamageInfo(int factionID, DamageInfo damageInfo) : this(factionID, damageInfo.Element, damageInfo.Damage, damageInfo.PenetrateShields, damageInfo.Knockback) { }

		public DamageInfo(int factionID, Element element, float damage, bool penetrateShields, float knockback) {
			FactionID = factionID;
			Element = element;
			Damage = damage;
			PenetrateShields = penetrateShields;
			Knockback = knockback;
		}
	}
}