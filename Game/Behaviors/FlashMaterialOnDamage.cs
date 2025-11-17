using PolyWare.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Game {
	public class FlashMaterialOnDamage : MonoBehaviour {
		[SerializeField] private GameObject damageReceiver;
		[SerializeField] private MeshRenderer[] meshRenderers;
		[SerializeField] private Color damageFlashColor;

		private List<Color> originalColors;
		
		private IDamageable damageable;
		
		private void Awake() {
			RecordOriginalColors();
			
			if (!damageReceiver.TryGetComponent(out damageable)) {
				Log.Error($"Flash requires a damageable (none found on {damageReceiver})");
			}
		}

		public void RecordOriginalColors() {
			originalColors = new List<Color>();
			foreach (MeshRenderer meshRenderer in meshRenderers) {
				originalColors.Add(meshRenderer.sharedMaterial.color);
			}
		}

		private void OnEnable() {
			damageable.OnHit += OnDamage;
		}

		private void OnDisable() {
			damageable.OnHit -= OnDamage;
		}

		private void OnDamage(DamageContext obj) {
			StartCoroutine(Flash(damageFlashColor));
		}

		private IEnumerator Flash(Color color) {
			foreach (MeshRenderer meshRenderer in meshRenderers) {
				meshRenderer.material.color = color;
			}

			yield return Yielders.WaitForSeconds(0.1f);

			// todo: temp skip to avoid conflicts with StunVisualHandler
			if (!damageable.IsStunned) {
				for (int i = 0; i < meshRenderers.Length; i++) {
					meshRenderers[i].material.color = originalColors[i];
				}
			}
		}
	}
}