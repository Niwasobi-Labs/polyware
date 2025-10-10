using System;
using PolyWare.Core;
using PolyWare.Core.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PolyWare.Audio {
	public class AudioService : MonoBehaviour, IAudioService {
		public bool IsPaused { get; private set; }

		[SerializeField] private SfxManager sfxManager;
		
		private void Awake() {
			ServiceLocator.Global.Register<IAudioService>(this);
		}
		
		public void SetPauseNewAudioSources(bool status) {
			IsPaused = status;
		}
		
		public void PlayRandomSfx(AudioClip[] audioClips, Transform playAt, float volume = 1f, bool loop = false) {
			PlaySfx(audioClips[Random.Range(0, audioClips.Length - 1)], playAt, volume, loop);
		}

		public void PlaySfx(AudioClip audioClip, Transform playAt, float volume = 1f, bool loop = false, Func<bool> stopCondition = null) {
			if (!audioClip || IsPaused) return;

			sfxManager.PlaySfx(audioClip, playAt, volume, loop,  stopCondition);
		}	
	}
}