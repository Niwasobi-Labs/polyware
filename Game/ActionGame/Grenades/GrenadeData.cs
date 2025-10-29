using PolyWare.Core.Entities;

namespace PolyWare.ActionGame.Grenades {
	public class GrenadeData : EntityData<GrenadeDefinition> {
		public GrenadeData(GrenadeDefinition definition) : base(definition) { }
	}
}