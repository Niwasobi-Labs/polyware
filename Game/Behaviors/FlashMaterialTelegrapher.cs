using System.Collections;
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class FlashMaterialTelegrapher : Telegrapher {
		[SerializeField] private MeshRenderer[] meshRenderers;
		[SerializeField] private float flashDelay = 0.2f;
		[SerializeField] private float flashDuration = 0.2f;
		[SerializeField] private Color flashColor = Color.red;

		private List<Color> originalColors;
		private bool isTelegraphing;
		Coroutine flashCoroutine;
		
		private void Awake() {
			RecordOriginalColors();
		}
		
		public override void StartTelegraphing(float duration) {
			isTelegraphing = true;
			if (flashCoroutine != null) StopCoroutine(flashCoroutine);
			flashCoroutine = StartCoroutine(Flash());
		}
		
		public override void StopTelegraphing() {
			isTelegraphing = false;
			if (flashCoroutine != null) StopCoroutine(flashCoroutine);
		}
		
		private void RecordOriginalColors() {
			originalColors = new List<Color>();
			foreach (MeshRenderer meshRenderer in meshRenderers) {
				originalColors.Add(meshRenderer.material.color);
			}
		}

		private void ResetColors() {
			for (int i = 0; i < meshRenderers.Length; i++) {
				meshRenderers[i].material.color = originalColors[i];
			}
		}	
		
		private void SwapToFlashColor() {
			for (int i = 0; i < meshRenderers.Length; i++) {
				meshRenderers[i].material.color = flashColor;
			}
		}	
		
		private IEnumerator Flash() {
			while (isTelegraphing) {
				
				SwapToFlashColor();
				
				yield return Yielders.WaitForSeconds(flashDuration);
			
				ResetColors();
				
				yield return Yielders.WaitForSeconds(flashDelay);	
			}
		}
	}
}