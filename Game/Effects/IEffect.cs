using System;
using PolyWare.Core;

namespace PolyWare.Game {
	public interface IEffect {
		event Action<IEffect> OnCompleted;
		public void Apply(IAffectable target, ContextHolder ctx);
		void Update(float deltaTime);
		public void Cancel();
	}
}