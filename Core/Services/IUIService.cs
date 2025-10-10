using PolyWare.UI;

namespace PolyWare.Core.Services {
	public interface IUIService : IService {
		public UIScreen GetTopScreen();
		public T PushScreen<T>(bool overlay, bool autoOpen = true) where T : UIScreen;
		public void RegisterWorldSpaceWidget(Widget widget);
	}
}