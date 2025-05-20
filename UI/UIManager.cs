using PolyWare.AssetManagement.Registries;
using PolyWare.Input;
using UnityEngine;

namespace PolyWare.UI {
	public abstract class UIManager : MonoBehaviour {
		[Header("UI System")]
		[SerializeField] protected UIInputHandler input;
		[SerializeField] protected ScreenRegistry screenRegistry;

		[SerializeField] private GameObject screenSpacePrefab;
		[SerializeField] private GameObject worldSpacePrefab;

		private readonly ScreenStack screenStack = new();

		private Canvas screenSpace;
		private Canvas worldSpace;

		public bool IsMouseActive { get; private set; }

		private void OnEnable() {
			input.OnDigitalNavigation += ChangeToControllerNavigation;
			input.OnMouseNavigation += ChangeToMouseNavigation;
		}
		
		private void OnDisable() {
			input.OnDigitalNavigation -= ChangeToControllerNavigation;
			input.OnMouseNavigation -= ChangeToMouseNavigation;
		}

		public void Initialize() {
			DontDestroyOnLoad(this);

			screenSpace = Instantiate(screenSpacePrefab).GetComponent<Canvas>();
			DontDestroyOnLoad(screenSpace);

			worldSpace = Instantiate(worldSpacePrefab).GetComponent<Canvas>();
			DontDestroyOnLoad(worldSpace);

			screenRegistry.Initialize();
			input.Initialize();
			
			OnInitialize();
		}

		protected abstract void OnInitialize();

		private void ChangeToMouseNavigation() {
			if (IsMouseActive) return;				
			
			Core.EventSystem.SetSelectedGameObject(null);
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
				Debug.Logger.Error($"Couldn't find screen of type: {typeof(T)}");
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

		// todo consider moving this to a IObserve interface? (https://lost-woods.codecks.io/card/18q-better-observer-pattern)
		private void OnDestroy() {
			OnDisable();
		}
	}
}