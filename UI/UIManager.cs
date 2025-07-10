using PolyWare.Core;
using PolyWare.Debug;
using PolyWare.Events;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace PolyWare.UI {
	public abstract class UIManager : MonoBehaviour {
		[Header("UI System")]
		[SerializeField] protected ScreenRegistry screenRegistry;

		[SerializeField] private GameObject screenSpacePrefab;
		[SerializeField] private GameObject worldSpacePrefab;

		private readonly ScreenStack screenStack = new();

		private Canvas screenSpace;
		private Canvas worldSpace;

		private EventBinding<DigitalNavigationEvent> digitalNavEventHandler;
		private EventBinding<MouseNavigationEvent> mouseNavEventHandler;
		public bool IsMouseActive { get; private set; }

		private void OnEnable() {
			digitalNavEventHandler = new EventBinding<DigitalNavigationEvent>(ChangeToControllerNavigation);
			EventBus<DigitalNavigationEvent>.Subscribe(digitalNavEventHandler);
			
			mouseNavEventHandler = new EventBinding<MouseNavigationEvent>(ChangeToMouseNavigation);
			EventBus<MouseNavigationEvent>.Subscribe(mouseNavEventHandler);
		}
		
		private void OnDisable() {
			EventBus<DigitalNavigationEvent>.Unsubscribe(digitalNavEventHandler);
			EventBus<MouseNavigationEvent>.Unsubscribe(mouseNavEventHandler);
		}

		public void Initialize() {
			DontDestroyOnLoad(this);

			screenSpace = Instantiate(screenSpacePrefab).GetComponent<Canvas>();
			DontDestroyOnLoad(screenSpace);

			worldSpace = Instantiate(worldSpacePrefab).GetComponent<Canvas>();
			DontDestroyOnLoad(worldSpace);

			screenRegistry.Initialize();
			
			OnInitialize();
		}

		protected abstract void OnInitialize();

		private void ChangeToMouseNavigation() {
			if (IsMouseActive) return;				
			
			Instance.EventSystem.SetSelectedGameObject(null);
			IsMouseActive = true;
				
			Cursor.visible = true;
		}
		
		private void ChangeToControllerNavigation() {
			if (!IsMouseActive) return;
			
			IsMouseActive = false;
			Cursor.visible = false;
			
			screenStack.GetTopScreen().Focus();
		}
		
		public T PushScreen<T>(bool overlay, bool autoOpen = true) where T : Screen {
			T screen = screenStack.CheckForCachedScreen(typeof(T)) as T ?? screenRegistry.GetPrefab(typeof(T), screenSpace.gameObject.transform) as T;

			if (!screen) {
				Log.Error($"Couldn't find screen of type: {typeof(T)}");
				return null;
			}
			
			screenStack.PushScreen(screen);

			if (autoOpen) screen.Open();
			else screen.Close(); 
			
			return screen;
		}

		public void RegisterWorldSpaceWidget(Widget widget) {
			widget.transform.SetParent(worldSpace.transform);
		}
	}
}