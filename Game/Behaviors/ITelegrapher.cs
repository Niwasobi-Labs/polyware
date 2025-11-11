using UnityEngine;

namespace PolyWare.Game {
	public interface ITelegrapher {
		public void StartTelegraphing(float duration);
		public void StopTelegraphing();
	}

	public abstract class Telegrapher : MonoBehaviour, ITelegrapher {
		public abstract void StartTelegraphing(float duration);
		public abstract void StopTelegraphing();
	}
}