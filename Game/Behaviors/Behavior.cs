using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public abstract class BehaviorFactory : IBehaviorFactory {
		public abstract IBehavior Create(ICharacter parent);
	}
	
	public abstract class Behavior : IBehavior {
		public bool IsRunning { get; private set; }
		protected readonly ICharacter parent;
		
		protected Behavior(ICharacter parent, BehaviorFactory factory) {
			this.parent = parent;
		}

		public void Start() {
			IsRunning = true;
			OnStart();
		}
		
		public void Tick(float dt) {
			OnTick(dt);	
		}

		public void Complete() {
			OnComplete();
			IsRunning = false;
		}
		
		protected abstract void OnStart();
		protected abstract void OnTick(float dt);
		protected abstract void OnComplete();
	}
}