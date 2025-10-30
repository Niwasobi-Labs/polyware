using System;

namespace PolyWare.Game {
	public class FuncStatModifier : StatModifier {
		private readonly StatType type;
		private readonly Func<float, float> operation;

		public FuncStatModifier(StatType type, float duration, Func<float, float> operation) : base(duration) {
			this.type = type;
			this.operation = operation;
		}

		public override void Handle(object sender, StatQuery statQuery) {
			if (statQuery.StatType == type) statQuery.Value = operation(statQuery.Value);
		}
	}
}