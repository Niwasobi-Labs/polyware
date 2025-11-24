namespace PolyWare.Core {
	public interface IInputService : IService {
		void ChangeToActionMap(int actionMap);
		void TogglePlayerInput(bool status);
		void ToggleUIInput(bool status);
	}
	
	public class NullInputService : IInputService {
		public void ChangeToActionMap(int actionMap) {}
		public void TogglePlayerInput(bool status) {}
		public void ToggleUIInput(bool status) {}
	}
}