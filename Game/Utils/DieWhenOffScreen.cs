using System.Collections;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	// must be put alongside a renderer
	public class DieWhenOffScreen : MonoBehaviour {
		[SerializeField] private GameObject target;
		[SerializeField] private float killTimer;
		
		private void OnBecameInvisible() {
			if (killTimer > 0 && gameObject.activeInHierarchy) StartCoroutine(KillTimer());
			else Kill();
		}

		private IEnumerator KillTimer() {
			yield return Yielders.WaitForSeconds(killTimer);
			
			Kill();
		}

		private void Kill() {
			Destroy(target);
		}
	}
}