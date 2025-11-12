using System;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class StunVisualHandler : MonoBehaviour {
		[Serializable]
		private class MeshColorSwapper {
			[SerializeField] private MeshRenderer meshRenderer;
			[SerializeField] private Color stunColor;

			private Color originalColor;

			public void Initialize() {
				originalColor = meshRenderer.material.color;
			}

			public void SwapColors(bool isStunned) {
				meshRenderer.material.color = isStunned ? stunColor : originalColor;
			}
		}
		
		[SerializeField] private GameObject target;
		[SerializeField] private GameObject[] hideOnStun;
		[SerializeField] private GameObject[] showOnStun;
		[SerializeField] private MeshColorSwapper[] colorChangesWhenStunned;
		
		private IDamageable damageable;
		
		private void Awake() {
			RecordOriginalColors();
			
			if (!target.TryGetComponent(out damageable)) {
				Log.Error($"Flash requires a damageable (none found on {target})");
			}
		}

		private void OnEnable() {
			damageable.OnStunStateChange += OnStun;
		}

		private void OnDisable() {
			damageable.OnStunStateChange -= OnStun;
		}
		
		private void RecordOriginalColors() {
			for (int i = 0; i < colorChangesWhenStunned.Length; i++) {
				colorChangesWhenStunned[i].Initialize();
			}
		}
		
		private void OnStun(bool isStunned) {
			if (isStunned) HandleStunned();
			else HandleUnstunned();
		}

		private void HandleStunned() {
			for (int i = 0; i < hideOnStun.Length; ++i) {
				hideOnStun[i].gameObject.SetActive(false);
			}
			
			for (int i = 0; i < showOnStun.Length; ++i) {
				showOnStun[i].gameObject.SetActive(true);
			}
			
			for (int i = 0; i < colorChangesWhenStunned.Length; i++) {
				colorChangesWhenStunned[i].SwapColors(true);
			}
		}
		
		private void HandleUnstunned() {
			for (int i = 0; i < hideOnStun.Length; ++i) {
				hideOnStun[i].gameObject.SetActive(true);
			}
			
			for (int i = 0; i < showOnStun.Length; ++i) {
				showOnStun[i].gameObject.SetActive(false);
			}
			
			for (int i = 0; i < colorChangesWhenStunned.Length; i++) {
				colorChangesWhenStunned[i].SwapColors(false);
			}
		}
	}
}