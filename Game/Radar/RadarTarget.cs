using UnityEngine;

namespace PolyWare.Game {
	public class RadarTarget : MonoBehaviour, IRadarTarget {
		public Sprite Sprite => sprite;
		public GameObject GameObject => gameObject;

		[SerializeField] private Sprite sprite;
		
		public bool ShouldBeDisplayed() {
			return true;
		}
	}
}