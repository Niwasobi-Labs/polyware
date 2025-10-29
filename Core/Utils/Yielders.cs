using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Utils {
	public class Yielders : MonoBehaviour {
		public static WaitForEndOfFrame WaitForEndOfFrame = new();

		private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsCache = new(100, new FloatComparer());
		private static readonly Dictionary<float, WaitForSecondsRealtime> WaitForSecondsRealtimeCache = new(100, new FloatComparer());

		public static WaitForSeconds WaitForSeconds(float timeToWait) {
			if (!WaitForSecondsCache.TryGetValue(timeToWait, out WaitForSeconds waitForSeconds)) WaitForSecondsCache.Add(timeToWait, waitForSeconds = new WaitForSeconds(timeToWait));
			return waitForSeconds;
		}

		public static WaitForSecondsRealtime WaitForSecondsRealtime(float timeToWaitRealtime) {
			if (!WaitForSecondsRealtimeCache.TryGetValue(timeToWaitRealtime, out WaitForSecondsRealtime waitForSecondsRealtime)) WaitForSecondsRealtimeCache.Add(timeToWaitRealtime, waitForSecondsRealtime = new WaitForSecondsRealtime(timeToWaitRealtime));
			return waitForSecondsRealtime;
		}

		private class FloatComparer : IEqualityComparer<float> {
			bool IEqualityComparer<float>.Equals(float x, float y) {
				return Mathf.Approximately(x, y);
			}

			int IEqualityComparer<float>.GetHashCode(float obj) {
				return obj.GetHashCode();
			}
		}
	}
}