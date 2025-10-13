using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Audio {
	public class SfxManager : MonoBehaviour {
		[SerializeField] private AudioSource sfxPrefab;
		[SerializeField] private AudioSource uiSfxPrefab;
		
		private Queue<AudioSource> sfxPool = new();
		private Queue<AudioSource> uiSfxPool = new();
		
		public AudioSource GetPrefab(AudioChannel channel) {
			switch (channel) {
				case AudioChannel.Sfx:
					return sfxPool.Count > 0 ? sfxPool.Dequeue() : Instantiate(sfxPrefab, transform);
				case AudioChannel.UISfx:
					return uiSfxPool.Count > 0 ? uiSfxPool.Dequeue() : Instantiate(uiSfxPrefab, transform);
				case AudioChannel.Music: // music should not be handled inside of the SFX manager
				default:
					return null;
			}
		}
		
		public void PlaySfx(AudioClip audioClip, Vector3 playAt, AudioChannel channel, float volume = 1f, bool loop = false, Func<bool> stopCondition = null) {
			AudioSource newAudioSource = GetPrefab(channel);
			newAudioSource.gameObject.SetActive(true);

			newAudioSource.gameObject.transform.SetPositionAndRotation(playAt, Quaternion.identity);

			newAudioSource.clip = audioClip;

			newAudioSource.volume = volume;

			newAudioSource.Play();

			if (stopCondition == null) {
				stopCondition = () => !loop && newAudioSource.time >= newAudioSource.clip.length;
			}
			else {
				var userCondition = stopCondition;
				stopCondition = () => userCondition() || (!loop && newAudioSource.time >= newAudioSource.clip.length);
			}

			StartCoroutine(StopWhenConditionMet(newAudioSource, channel, stopCondition));
		}	
		
		private IEnumerator StopWhenConditionMet(AudioSource source, AudioChannel channel, Func<bool> condition) {
			while (source && !condition()) yield return null;

			if (!source) yield break;

			source.Stop();
			ReleaseAudioSource(source, channel);
		}

		private void ReleaseAudioSource(AudioSource source, AudioChannel channel) {
			source.Stop();
			source.clip = null;
			source.loop = false;
			source.gameObject.SetActive(false);
			switch (channel) {
				case AudioChannel.Sfx:
					sfxPool.Enqueue(source);
					break;
				case AudioChannel.UISfx:
					uiSfxPool.Enqueue(source);
					break;
				case AudioChannel.Music:
				default:
					throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
			}
		}
		
		
	}
}