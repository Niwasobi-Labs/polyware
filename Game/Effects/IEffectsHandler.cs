using System;

namespace PolyWare.Effects {
	public interface IEffectsHandler {
		public event Action OnEmpty;
		
		public void Add(IEffect effect);
		public void Update(float deltaTime);
		public void Remove(IEffect effect);
		public void RemoveAll();
		public bool IsEmpty { get; }
	}
}