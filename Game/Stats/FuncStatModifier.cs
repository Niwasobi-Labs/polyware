using System;

namespace PolyWare.Game {
	public class FuncStatModifier : StatModifier {
		private readonly CharacterStatType types;
		private readonly Func<float, float> operation;

		public FuncStatModifier(CharacterStatType types, float duration, Func<float, float> operation) : base(duration) {
			this.types = types;
			this.operation = operation;
		}

		public override void Handle(object sender, StatQuery statQuery) {
			if (statQuery.CharacterStatType == types) statQuery.Value = operation(statQuery.Value);
		}
	}
}