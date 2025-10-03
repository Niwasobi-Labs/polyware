using System;
using PolyWare.Abilities;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Combat {
	[Serializable]
	public struct DamageContext : IContext {
		[HideInInspector] public GameObject Culprit;
		[HideInInspector] public AbilityDefinition Ability;
		public float Damage;
		public Element Element;

		public DamageContext(GameObject culprit, DamageContext damageContext, AbilityDefinition ability) : this(culprit, damageContext.Element, damageContext.Damage, ability) { }

		public DamageContext(GameObject culprit, Element element, float damage, AbilityDefinition ability) {
			Culprit = culprit;
			Element = element;
			Damage = damage;
			Ability = ability;
		}
	}
}