using UnityEngine;

namespace PolyWare.Gameplay {
	public interface IRadarTarget {
		public GameObject GameObject { get; }
		public Sprite Sprite { get; }
		public bool ShouldBeDisplayed();
	}
}