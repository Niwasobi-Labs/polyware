using FMODUnity;
using UnityEngine;

namespace PolyWare.Core {
	public class FMODAudioService : MonoBehaviour, IAudioService {
		public bool IsPaused { get; private set; }
		
		protected StudioListener listener;
		
		private void Awake() {
			ServiceLocator.Global.Register<IAudioService>(this);
		}
		
		public void SetPauseNewAudioSources(bool status) {
			IsPaused = status;
		}
		
		public void PlayOneShot(EventReference sound, Vector3 worldPos) {
			if (IsPaused || sound.IsNull) return;
			
			RuntimeManager.PlayOneShot(sound, worldPos);
		}

		// todo: consider pooling if needed
		public IAudioEventInstance GetInstance(EventReference sound) {
			return new FMODEventInstance(sound);
		}
	}
}