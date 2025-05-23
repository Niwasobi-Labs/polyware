using System;
using UnityEngine;

namespace PolyWare.Combat {
	[Serializable]
	public struct DamageInfo {
		[HideInInspector] public readonly bool FromPlayer;
		public int Damage;
		public Element Element;
		public bool PenetrateShields;

		public DamageInfo(bool isPlayer, DamageInfo damageInfo) : this(isPlayer, damageInfo.Element, damageInfo.Damage, damageInfo.PenetrateShields) { }

		public DamageInfo(bool fromPlayer, Element element, int damage, bool penetrateShields) {
			FromPlayer = fromPlayer;
			Element = element;
			Damage = damage;
			PenetrateShields = penetrateShields;
		}
	}
}