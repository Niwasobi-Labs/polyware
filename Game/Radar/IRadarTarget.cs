using UnityEngine;

namespace PolyWare.Game {
	public interface IRadarTarget {
		public GameObject GameObject { get; }
		public Sprite Sprite { get; }
		public bool ShouldBeDisplayed();
	}
}