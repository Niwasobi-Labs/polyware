namespace PolyWare.Core {
	public interface IUIService : IService {
		public T PushScreen<T>(int layer) where T : UIScreen;
		public UIScreen GetTopScreen();
		public void ClearLayer(int layer);
		public void ClearHistory();
		public void PopScreen();
		public void RegisterWorldSpaceWidget(Widget widget);
	}
}