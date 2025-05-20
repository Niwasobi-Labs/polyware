using System.Collections;
using System.Collections.Generic;
using PolyWare.AssetManagement;
using PolyWare.Audio;
using PolyWare.Input;
using PolyWare.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PolyWare {
	public abstract class Bootstrapper : MonoBehaviour {
		[Header("PolyWare")]
		// scriptable systems
		public Constants Constants;
		public Collector Collector;
		
		// prefabs systems
		public EventSystem EventSystem;
		public InputManager Input;
		public SfxManager SfxManager;
		public UIManager UI;
		public Game Game;
		
		private void Awake() {
			DontDestroyOnLoad(Input = Instantiate(Input));
			DontDestroyOnLoad(EventSystem = Instantiate(EventSystem));
			DontDestroyOnLoad(SfxManager = Instantiate(SfxManager));
			DontDestroyOnLoad(UI = Instantiate(UI));
			DontDestroyOnLoad(Game = Instantiate(Game));
			
			Core.Setup(this);
			
			OnAwake();
			
			Core.Initialize();
		}

		protected abstract void OnAwake();
		
		protected void Start() {
			Game.LoadGame();
			StartCoroutine(SlowStart());
		}

		private IEnumerator SlowStart() {
			// wait a frame for load to finish (and Awake to be called on all objects)
			yield return null;
			Game.StartGame();
		}
	}
}