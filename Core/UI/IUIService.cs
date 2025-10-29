using PolyWare.Core.Services;

namespace PolyWare.UI {
	public interface IUIService : IService {
		public UIScreen GetTopScreen();
		public T PushScreen<T>(bool overlay, bool autoOpen = true) where T : UIScreen;
		public void RegisterWorldSpaceWidget(Widget widget);
	}
}