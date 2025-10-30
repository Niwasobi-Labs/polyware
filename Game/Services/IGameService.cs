using PolyWare.Core;

namespace PolyWare.Game {
	public interface IGameService : IService {
		public Constants GameConstants { get; }
		public ICharacter GetLocalPlayerCharacter { get; }
		public void StartGame();
		public void RestartCurrentLevel();
		public void QuickCompleteCurrentLevel();
	}
}