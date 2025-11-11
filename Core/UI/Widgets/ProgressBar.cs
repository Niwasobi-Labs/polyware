using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace PolyWare.Core {
	public class ProgressBar : MonoBehaviour {
		[SerializeField] private Image fill;
		[SerializeField] private bool animate = false;
		[SerializeField] private ShakeSettings shakeAnimation;
		private Color originalFillColor;

		private void Awake() {
			originalFillColor = fill.color;
		}

		public void SetColor(Color color) {
			fill.color = color;
		}

		public void ResetColor() {
			fill.color = originalFillColor;
		}

		public void SetProgress(float progress) {
			if (fill.fillAmount > progress && animate) {
				Tween.PunchLocalPosition(transform, shakeAnimation);
			}
			fill.fillAmount = float.IsNaN(progress) ? 0 : progress;
		}
	}
}