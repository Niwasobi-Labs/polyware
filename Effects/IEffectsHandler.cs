namespace PolyWare.Effects {
	public interface IEffectsHandler {
		public void Add(IEffect effect);
		public void Update(float deltaTime);
		public void Remove(IEffect effect);
	}
}