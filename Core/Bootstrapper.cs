using System;
using System.Collections;
using PolyWare.Utils;
using PolyWare.AssetManagement;
using PolyWare.Audio;
using PolyWare.Cameras;
using PolyWare.Debug;
using PolyWare.Input;
using PolyWare.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PolyWare.Core {
	public abstract class Bootstrapper : MonoBehaviour {
		[Header("PolyWare")]
		// scriptable systems
		public Constants Constants;
		public Collector Collector;
		
		// prefabs systems
		public InputManager Input;
		public SFXManager SfxManager;
		public UIManager UI;
		public GameManager GameManager;
		public GameObject CameraPrefab;
		public ICameraManager CameraManager; // todo: can we serialize interface objects? (odin?)
		
		private void Awake() {
			DontDestroyOnLoad(Input = Instantiate(Input));
			DontDestroyOnLoad(SfxManager = Instantiate(SfxManager));
			DontDestroyOnLoad(UI = Instantiate(UI));
			DontDestroyOnLoad(GameManager = Instantiate(GameManager));

			GameObject cameraManager = Instantiate(CameraPrefab);
			if (!cameraManager.TryGetComponent(out CameraManager)) {
				Log.Error("Invalid Camera Manager prefab");
				throw new ArgumentException();
			}
			
			DontDestroyOnLoad(cameraManager);
			
			Instance.Setup(this);
			
			OnAwake();
			
			Instance.Initialize();
		}

		protected abstract void OnAwake();
		
		protected void Start() {
			GameManager.LoadGame();
			StartCoroutine(SlowStart());
		}

		private IEnumerator SlowStart() {
			// wait a frame for load to finish (and Awake to be called on all objects)
			yield return null;
			GameManager.StartGame();
		}
	}
}