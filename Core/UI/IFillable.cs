using UnityEngine;

namespace Alpaca.UI {
	public interface IFillable {
		public GameObject GameObject { get; } 
		public void SetFillAmount(float newFillAmount);
	}
}