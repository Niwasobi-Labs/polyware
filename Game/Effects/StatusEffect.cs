using System;
using PolyWare.Core;

namespace PolyWare.Game {
	public enum StatusEffectType {
		Vulnerable,
		Slowed,
		Weakened
	}

	[Serializable]
	public class StatusEffectFactory : IEffectFactory {
		public StatusEffectType StatusEffectType;
		public float Stack = 1;
		public float Duration;

		public IEffect Create() {
			return new StatusEffect {
				StatusEffectType = StatusEffectType,
				Stack = Stack,
				Duration = Duration
			};
		}
	}

	public struct StatusEffect : IEffect {
		public event Action<IEffect> OnCompleted;
		
		public StatusEffectType StatusEffectType;
		public float Stack;
		public float Duration;
		
		private CountdownTimer timer;
		
		public void Apply(IAffectable target, ContextHolder ctx) {
			if (Duration > 0) {
				timer = new CountdownTimer(Duration);
				timer.OnTimerComplete += Complete;
				timer.Start();	
			}
		}
		
		public void Update(float deltaTime) {
			timer?.Tick(deltaTime);
		}
		
		private void Complete() {
			OnCompleted?.Invoke(this);
		}
		
		public void Cancel() {
			timer.Stop();
			Complete();
		}
	}
}