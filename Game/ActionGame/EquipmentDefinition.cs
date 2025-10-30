using UnityEngine;

namespace PolyWare.Game {
	public abstract class EquipmentDefinition : ItemDefinition {

		[field: SerializeField] public MeleeDefinition.MeleeAttackInfo MeleeInfo { get; private set; }
	}
}