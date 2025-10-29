using System.Collections;
using UnityEngine;

namespace PolyWare.Gameplay {
	// must be put alongside a renderer
	public class DieWhenOffScreen : MonoBehaviour {
		[SerializeField] private GameObject target;
		[SerializeField] private float killTimer;
		
		private void OnBecameInvisible() {
			if (killTimer > 0 && gameObject.activeInHierarchy) StartCoroutine(KillTimer());
			else Kill();
		}

		private IEnumerator KillTimer() {
			yield return Utils.Yielders.WaitForSeconds(killTimer);
			
			Kill();
		}

		private void Kill() {
			Destroy(target);
		}
	}
}