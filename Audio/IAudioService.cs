using System;
using PolyWare.Core.Services;
using UnityEngine;

namespace PolyWare.Audio {
	public enum AudioChannel {
		Music,
		Sfx,
		UISfx
	}
	
	public interface IAudioService : IService {
		public void SetPauseNewAudioSources(bool status);
		public void PlayRandomSfx(AudioClip[] audioClips, Vector3 playAt, AudioChannel channel, float volume = 1f, bool loop = false);
		public void PlaySfx(AudioClip audioClip, Vector3 playAt, AudioChannel channel, float volume = 1f, bool loop = false, Func<bool> stopCondition = null);
	}
}