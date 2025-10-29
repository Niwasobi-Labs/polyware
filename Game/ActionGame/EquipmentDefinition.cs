using PolyWare.Combat;
using PolyWare.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.ActionGame {
	public abstract class EquipmentDefinition : ItemDefinition {

		[field: SerializeField] public MeleeDefinition.MeleeAttackInfo MeleeInfo { get; private set; }
	}
}