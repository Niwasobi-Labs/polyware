using System;
using PolyWare.Core.Services;
using UnityEngine;

namespace PolyWare.Audio {
	public interface IAudioService : IService {
		public void SetPauseNewAudioSources(bool status);
		public void PlayRandomSfx(AudioClip[] audioClips, Transform playAt, float volume = 1f, bool loop = false);
		public void PlaySfx(AudioClip audioClip, Transform playAt, float volume = 1f, bool loop = false, Func<bool> stopCondition = null);
	}
}