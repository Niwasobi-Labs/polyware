using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class ShrinkOnDamageUniformly : MonoBehaviour {
		[SerializeField] private GameObject damageTarget;
		[SerializeField] private float scaleStart = 2f;
		[SerializeField] private float scaleEnd = 1f;
		[SerializeField] private AnimationCurve scaleCurve = AnimationCurve.Linear(0, 0, 1, 1);
		
		private IDamageable damageable;
		private Vector3 startingScale;
		private Vector3 endingScale;
		
		private void Awake() {
			if (!damageTarget.TryGetComponent(out damageable)) {
				Log.Error("Can't find damageable on DamageTarget");
			}

			// assume uniform-scaling
			startingScale = Vector3.one * scaleStart;
			endingScale = Vector3.one * scaleEnd;
			damageTarget.transform.localScale = startingScale;
		}

		private void OnEnable() {
			damageable.OnHit += Shrink;
		}

		private void OnDisable() {
			damageable.OnHit -= Shrink;
		}

		private void Shrink(DamageContext obj) {
			damageTarget.transform.localScale = Vector3.Lerp(startingScale, endingScale, 1f - damageable.CurrentHealthPercentage);
		}
	}
}