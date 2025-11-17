using UnityEngine;

namespace PolyWare.Game {
	[CreateAssetMenu(menuName = "PolyWare/Definitions/Entity", fileName = "New Entity")]
	public class SimpleEntityDefinition : EntityDefinition {
		public override IEntityData CreateDefaultInstance() {
			return new EntityData(this);
		}
	}
}