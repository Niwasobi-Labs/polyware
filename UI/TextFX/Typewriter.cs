using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.UI.TextFX {
	public class Typewriter : MonoBehaviour {
		[SerializeField] [FormerlySerializedAs("Text")] private TMP_Text text;

		[SerializeField] [FormerlySerializedAs("TriggerOnEnable")]
		private bool triggerOnEnable;

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
			while (text.maxVisibleCharacters != totalCharacters) {
				text.maxVisibleCharacters += 1;
				yield return PolyWare.Utils.Yielders.WaitForSeconds(speedUp ? PolyWare.Core.Constants.dialogueSkipMultiplier : PolyWare.Core.Constants.dialogueCharacterDelay);
			}
		}
	}
}