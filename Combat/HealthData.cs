using System;
using Sirenix.OdinInspector;

namespace PolyWare.Combat {
	[Serializable]
	public struct HealthData {
		public float Max;
		[ReadOnly] public float Current;
		public bool Invincible;
		public bool CanHeal;
			
		public bool CanRegen;
		[ShowIf("CanRegen")] public float RegenDelayTime;
		[ShowIf("CanRegen")] public float RegenTime;	
	}
}