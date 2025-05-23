using System;
using System.Collections.Generic;
using PolyWare.Timers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Combat {
	[Serializable]
	public class DamageableValueHandler {

		[Serializable]
		public struct DamageValueHandlerData {
			public float Max;
			[ReadOnly] public float Current;
			public bool Invincible;
			public bool CanHeal;
			public bool CanRegen;
			
			[ShowIf("CanRegen")] public float RegenDelayTime;
			[ShowIf("CanRegen")] public float RegenTime;	
		}

		[ShowInInspector] private DamageValueHandlerData data;
		
		public UnityAction OnValueDepleted = delegate {};
		public UnityAction OnRegenStarted = delegate {};
		public UnityAction<float> OnValueChanged = delegate {};
			
		public CountdownTimer RegenTimer;
		public CountdownTimer RegenDelayTimer;
			
		protected List<Timer> timers;

		public float Max => data.Max;
		public float Current => data.Current;
		
		public DamageableValueHandler(DamageValueHandlerData initData) {
			data = initData;
			data.Current = initData.Max;
			SetupTimers();
		}
		
		private void SetupTimers() {
			RegenTimer = new CountdownTimer(data.RegenTime);
			RegenTimer.OnTimerStart += () => OnRegenStarted.Invoke();
			RegenTimer.OnTimerTick  += () => {
				data.Current = Mathf.Lerp(0, Max, 1 - RegenTimer.Progress);
				OnValueChanged.Invoke(data.Current / data.Max);
			};
			RegenTimer.OnTimerComplete += () => {
				data.Current = Max;
				OnValueChanged.Invoke(1);
			};
			
			RegenDelayTimer = new CountdownTimer(data.RegenDelayTime) {
				OnTimerComplete = () => RegenTimer.StartAt(1 - data.Current / data.Max)
			};
			
			timers = new List<Timer>(2) { RegenTimer, RegenDelayTimer };
		}

		public void TakeDamage(float damage) {
			if (data.Invincible) return;

			if (data.CanRegen) {
				RegenTimer.Stop();
				RegenDelayTimer.Restart();
			}
			
			data.Current = Mathf.Max(data.Current - damage, 0);
			
			OnValueChanged.Invoke(data.Current / data.Max);
			
			if (data.Current <= 0) OnValueDepleted.Invoke();
		}

		private void UpdateValueRegen() {
			
		}
		
		public void Heal(float healAmount) {
			if (!data.CanHeal) return;
			
			data.Current = Mathf.Max(data.Current + healAmount, data.Max);
			OnValueChanged.Invoke(data.Current / data.Max);
		}
		
		public void TickTimers(float deltaTime) {
			foreach (Timer timer in timers) timer.Tick(deltaTime);
		}
	}
}