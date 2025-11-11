using System.Collections;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class FlashSpriteTelegrapher : Telegrapher {
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private float flashDelay = 0.2f;
		[SerializeField] private float flashDuration = 0.2f;
		[SerializeField] private Color flashColor = Color.red;

		private Color defaultColor;
		private bool isTelegraphing;
		
		private void Awake() {
			defaultColor = sprite.color;
		}

		public override void StartTelegraphing(float duration) {
			isTelegraphing = true;
			StopAllCoroutines();
			StartCoroutine(Flash());
		}
		
		public override void StopTelegraphing() {
			isTelegraphing = false;
		}
		
		private IEnumerator Flash() {
			while (isTelegraphing) {
				sprite.color = flashColor;
				
				yield return Yielders.WaitForSeconds(flashDuration);
			
				sprite.color = defaultColor;
				
				yield return Yielders.WaitForSeconds(flashDelay);	
			}
		}
	}
}