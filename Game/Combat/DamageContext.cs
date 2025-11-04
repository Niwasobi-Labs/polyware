using System;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public struct DamageContext : IContext {
		[HideInInspector] public AbilityDefinition Ability;
		public GameObject Culprit;
		public float Damage;
		public Element Element;

		public DamageContext(DamageContext damageContext, GameObject culprit, AbilityDefinition ability) : this(damageContext.Element, damageContext.Damage, culprit, ability) { }

		public DamageContext(Element element, float damage, GameObject culprit, AbilityDefinition ability) {
			Element = element;
			Damage = damage;
			Culprit =  culprit;
			Ability = ability;
		}
	}
}