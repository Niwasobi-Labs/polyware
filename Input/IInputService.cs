using PolyWare.Core.Services;

namespace PolyWare.Input {
	public interface IInputService : IService {
		void ChangeToActionMap(int actionMap);
	}
	
	public class NullInputService : IInputService {
		public void ChangeToActionMap(int actionMap) {}
	}
}