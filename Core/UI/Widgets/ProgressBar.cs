using UnityEngine;
using UnityEngine.UI;

namespace PolyWare.UI.Widgets {
	public class ProgressBar : MonoBehaviour {
		[SerializeField] private Image fill;

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
			fill.fillAmount = float.IsNaN(progress) ? 0 : progress;
		}
	}
}