using System.Collections.Generic;
using PolyWare.Abilities;
using PolyWare.Core.Entities;
using PolyWare.Items;
using PolyWare.Stats;
using UnityEngine;

namespace PolyWare.ActionGame.PowerUps {
	[CreateAssetMenu(fileName = "New PowerUp", menuName = "PolyWare/Items/PowerUp")]
	public class PowerUpDefinition : ItemDefinition {
		[field: SerializeField] public Ability OnPickupAbility; 
		[field: SerializeField] public List<StatModiferData> StatData;
		[field: SerializeField] public float LifeTime { get; private set; }
		
		public override IEntityData CreateDefaultInstance() => new PowerUpData(this);
	}
}