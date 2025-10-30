using FMODUnity;
using UnityEngine;

namespace PolyWare.Core {
	public enum AudioChannel {
		Music,
		Sfx,
		UISfx
	}
	
	public interface IAudioService : IService {
		public void SetAttenuationObject(GameObject newAttenuationObject);
		public void SetPauseNewAudioSources(bool status);
		// public void PlayRandomSfx(AudioClip[] audioClips, Vector3 playAt, AudioChannel channel, float volume = 1f, bool loop = false);
		// public void PlaySfx(AudioClip audioClip, Vector3 playAt, AudioChannel channel, float volume = 1f, bool loop = false, Func<bool> stopCondition = null);
		public void PlayOneShot(EventReference sound, Vector3 worldPosition);
		public IAudioEventInstance GetInstance(EventReference sound);
	}
}