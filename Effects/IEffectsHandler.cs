using PolyWare.Combat;

namespace PolyWare.Effects {
	public interface IEffectsHandler {
		public void Add(IEffect<IDamageable> effect);
		public void Update(float deltaTime);
		public void Remove(IEffect<IDamageable> effect);
	}
}