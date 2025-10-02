using System;
using PolyWare.Timers;
using Unity.VisualScripting.YamlDotNet.Core;

namespace PolyWare.Stats {
	public abstract class StatModifier : IDisposable {
		
		public bool MarkedForRemoval { get; private set; }
		
		public event Action<StatModifier> OnDispose =  delegate { };
		public abstract void Handle(object sender, StatQuery statQuery);

		private readonly CountdownTimer timer;

		public static StatModifier Create(StatModiferData data) {
			return data.Type switch {
				StatModiferData.OperatorType.Add => new BasicStatModifier(data.StatType, data.Duration, v => v + data.Value),
				StatModiferData.OperatorType.Multiply => new BasicStatModifier(data.StatType, data.Duration, v => v * data.Value),
				_ => throw new ArgumentOutOfRangeException()
			};	
		}
		
		protected StatModifier(float duration) {
			if (duration <= 0) return;
			
			timer = new CountdownTimer(duration);
			timer.OnTimerComplete += () => MarkedForRemoval = true;
			timer.Start();
		}

		public void Update(float deltaTime) {
			timer.Tick(deltaTime);
		}
		
		public void Dispose() {
			OnDispose.Invoke(this);
		}
	}
}