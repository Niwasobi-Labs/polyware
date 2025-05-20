using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PolyWare.Audio {
	public class SfxManager : MonoBehaviour {
		[SerializeField] private AudioSource sFXPrefab;

		private readonly Queue<AudioSource> audioSourcePool = new();

		public void PlayRandomClip(AudioClip[] audioClips, Transform playAt, float volume = 1f, bool loop = false) {
			PlayClip(audioClips[Random.Range(0, audioClips.Length - 1)], playAt, volume, loop);
		}

		public void PlayClip(AudioClip audioClip, Transform playAt, float volume = 1f, bool loop = false, Func<bool> stopCondition = null) {
			if (!audioClip) return;
			
			AudioSource newAudioSource = audioSourcePool.Count > 0 ? audioSourcePool.Dequeue() : Instantiate(sFXPrefab, transform);
			newAudioSource.gameObject.SetActive(true);

			newAudioSource.gameObject.transform.SetPositionAndRotation(playAt.position, playAt.rotation);

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

			StartCoroutine(StopWhenConditionMet(newAudioSource, stopCondition));
		}

		private IEnumerator StopWhenConditionMet(AudioSource source, Func<bool> condition) {
			while (source && !condition()) yield return null;

			if (!source) yield break;

			source.Stop();
			ReleaseAudioSource(source);
		}

		private void ReleaseAudioSource(AudioSource source) {
			source.Stop();
			source.clip = null;
			source.loop = false;
			source.gameObject.SetActive(false);
			audioSourcePool.Enqueue(source);
		}
	}
}