using PolyWare.Core.Entities;
using PolyWare.Items;
using UnityEngine;

namespace PolyWare.ActionGame.PowerUps {
	[CreateAssetMenu(fileName = "New PowerUp", menuName = "PolyWare/Items/PowerUp")]
	public class PowerUpDefinition : ItemDefinition {
		public override IEntityData CreateDefaultInstance() => new EntityData<PowerUpDefinition>(this);
	}
}