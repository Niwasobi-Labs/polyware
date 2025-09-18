using UnityEngine;

namespace PolyWare.Utils {
	public class VectorUtils {
		public static Vector3 PointBetween(Vector3 a, Vector3 b) {
			return (a + b) / 2f;
		}
	}
}