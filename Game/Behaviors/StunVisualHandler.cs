using System;
using PolyWare.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	public class StunVisualHandler : MonoBehaviour {
		[Serializable]
		private class MeshColorSwapper {
			public enum SwapMode {
				None,
				Material,
				Color
			}
			[SerializeField] private MeshRenderer meshRenderer;
			[SerializeField] private SwapMode swapMode;
			[SerializeField][ShowIf("swapMode", SwapMode.Color)] private Color stunColor;
			[SerializeField][ShowIf("swapMode", SwapMode.Material)] private Material stunMaterial;

			private Color originalColor;
			private Material originalMaterial;

			public void Initialize() {
				originalColor = meshRenderer.material.color;
				originalMaterial = meshRenderer.sharedMaterial;
			}

			public void SwapVisual(bool isStunned) {
				switch (swapMode) {
					case SwapMode.Material:
						SwapMaterials(isStunned);
						break;
					case SwapMode.Color:
						SwapColors(isStunned);
						break;
					case SwapMode.None:
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			
			private void SwapColors(bool isStunned) {
				meshRenderer.material.color = isStunned ? stunColor : originalColor;
			}
			
			private void SwapMaterials(bool isStunned) {
				meshRenderer.sharedMaterial = isStunned ? stunMaterial : originalMaterial;
			}
		}
		
		[SerializeField] private GameObject target;
		[SerializeField] private GameObject[] hideOnStun;
		[SerializeField] private GameObject[] showOnStun;
		[SerializeField] private MeshColorSwapper[] meshVisualSwappers;
		
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
			for (int i = 0; i < meshVisualSwappers.Length; i++) {
				meshVisualSwappers[i].Initialize();
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
			
			for (int i = 0; i < meshVisualSwappers.Length; i++) {
				meshVisualSwappers[i].SwapVisual(true);
			}
		}
		
		private void HandleUnstunned() {
			for (int i = 0; i < hideOnStun.Length; ++i) {
				hideOnStun[i].gameObject.SetActive(true);
			}
			
			for (int i = 0; i < showOnStun.Length; ++i) {
				showOnStun[i].gameObject.SetActive(false);
			}
			
			for (int i = 0; i < meshVisualSwappers.Length; i++) {
				meshVisualSwappers[i].SwapVisual(false);
			}
		}
	}
}