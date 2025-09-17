using System;

namespace PolyWare.Stats {
	public class BasicStatModifier : StatModifier {
		private readonly StatType type;
		private readonly Func<float, float> operation;

		public BasicStatModifier(StatType type, float duration, Func<float, float> operation) : base(duration) {
			this.type = type;
			this.operation = operation;
		}

		public override void Handle(object sender, StatQuery statQuery) {
			if (statQuery.StatType == type) statQuery.Value = operation(statQuery.Value);
		}
	}
}