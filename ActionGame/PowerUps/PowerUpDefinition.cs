using FMODUnity;
using PolyWare.Abilities;
using PolyWare.Core.Entities;
using PolyWare.Items;
using UnityEngine;

namespace PolyWare.ActionGame.PowerUps {
	[CreateAssetMenu(menuName = "PolyWare/Definitions/PowerUp", fileName = "New PowerUp")]
	public class PowerUpDefinition : ItemDefinition {
		[field: SerializeField] public AbilityDefinition OnPickupAbility; 
		[field: SerializeField] public EventReference PickupSound;
		[field: SerializeField] public float LifeTime { get; private set; }
		
		public override IEntityData CreateDefaultInstance() => new PowerUpData(this);
	}
}