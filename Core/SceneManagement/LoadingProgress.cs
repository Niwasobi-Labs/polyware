using System;

namespace PolyWare.Core.SceneManagement {
	public class LoadingProgress : IProgress<float> {
		public event Action<float> OnProgress;

		private const float ratio = 1f;

		public void Report(float value) {
			OnProgress?.Invoke(value / ratio);
		}
	}
}