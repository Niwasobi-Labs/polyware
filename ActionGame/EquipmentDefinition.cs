using PolyWare.Combat;
using PolyWare.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.ActionGame {
	public abstract class EquipmentDefinition : ItemDefinition {
		[field: Title("Melee")]
		[field: SerializeField] public float MeleeCooldown { get; private set; }
		[field: SerializeField] public DamageInfo MeleeDamage { get; private set; }
		[field: SerializeField] public AudioClip MeleeSound { get; private set; }
	}
}