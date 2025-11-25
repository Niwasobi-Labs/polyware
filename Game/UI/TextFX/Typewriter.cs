using PolyWare.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.Game {
	public class Typewriter : MonoBehaviour {
		[SerializeField] private TMP_Text text;

		[SerializeField] private bool triggerOnEnable;
		[SerializeField] private bool useUnscaledDeltaTime;

		private bool speedUp;

		private void OnEnable() {
			if (triggerOnEnable) StartRevealing(text.text);
		}

		public void StartRevealing(string message) {
			speedUp = false;
			text.text = message;
			text.maxVisibleCharacters = 0;
			StartCoroutine(Reveal(message.Length));
		}

		public void Skip(bool instant) {
			if (instant) {
				StopAllCoroutines();
				text.maxVisibleCharacters = text.text.Length;
			}
			else {
				speedUp = true;
			}
		}

		private IEnumerator Reveal(int totalCharacters) {
			var gameService = ServiceLocator.Global.Get<IGameService>();
			while (text.maxVisibleCharacters != totalCharacters) {
				text.maxVisibleCharacters += 1;
				if (useUnscaledDeltaTime) {
					yield return Yielders.WaitForSecondsRealtime(speedUp ? gameService.GameConstants.DialogueSkipMultiplier : gameService.GameConstants.DialogueCharacterDelay);
				}
				else {
					yield return Yielders.WaitForSeconds(speedUp ? gameService.GameConstants.DialogueSkipMultiplier : gameService.GameConstants.DialogueCharacterDelay);	
				}
			}
		}
	}
}