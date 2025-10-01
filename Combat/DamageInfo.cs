using System;
using PolyWare.Effects;
using UnityEngine;

namespace PolyWare.Combat {
	[Serializable]
	public struct DamageInfo {
		[HideInInspector] public int FactionID;
		public float Damage;
		public Element Element;

		public DamageInfo(int factionID, DamageInfo damageInfo) : this(factionID, damageInfo.Element, damageInfo.Damage) { }

		public DamageInfo(int factionID, Element element, float damage) {
			FactionID = factionID;
			Element = element;
			Damage = damage;
		}
	}
}