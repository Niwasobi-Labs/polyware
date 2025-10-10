using PolyWare.Characters;
using PolyWare.Levels;
using PolyWare.Utils;

namespace PolyWare.Core.Services {
	public interface IGameService : IService {
		public Constants GameConstants { get; }
		public ICharacter GetLocalPlayerCharacter { get; }
		public void StartGame();
		public void RestartCurrentLevel();
	}
}