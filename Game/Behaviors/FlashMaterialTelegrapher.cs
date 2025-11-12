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
		
		private void Awake() {
			RecordOriginalColors();
		}
		
		public override void StartTelegraphing(float duration) {
			isTelegraphing = true;
			StopAllCoroutines();
			StartCoroutine(Flash());
		}
		
		public override void StopTelegraphing() {
			isTelegraphing = false;
		}
		
		private void RecordOriginalColors() {
			originalColors = new List<Color>();
			foreach (MeshRenderer meshRenderer in meshRenderers) {
				originalColors.Add(meshRenderer.material.color);
			}
		}
		
		private IEnumerator Flash() {
			while (isTelegraphing) {
				for (int i = 0; i < meshRenderers.Length; i++) {
					meshRenderers[i].material.color = flashColor;
				}
				
				yield return Yielders.WaitForSeconds(flashDuration);
			
				for (int i = 0; i < meshRenderers.Length; i++) {
					meshRenderers[i].material.color = originalColors[i];
				}
				
				yield return Yielders.WaitForSeconds(flashDelay);	
			}
		}
	}
}