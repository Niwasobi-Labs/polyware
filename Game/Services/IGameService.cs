using PolyWare.Characters;
using PolyWare.Core.Services;
using PolyWare.Utils;

namespace PolyWare.Core.Game {
	public interface IGameService : IService {
		public Constants GameConstants { get; }
		public ICharacter GetLocalPlayerCharacter { get; }
		public void StartGame();
		public void RestartCurrentLevel();
		public void QuickCompleteCurrentLevel();
	}
}