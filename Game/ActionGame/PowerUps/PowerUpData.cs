using PolyWare.Core.Entities;

namespace PolyWare.ActionGame.PowerUps {
	public class PowerUpData : EntityData<PowerUpDefinition> {
		public PowerUpData(PowerUpDefinition definition) : base(definition) { }
	}
}