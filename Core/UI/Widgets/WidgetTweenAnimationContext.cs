using System;
using PrimeTween;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Core {
	[Serializable]
	public class WidgetTweenAnimationContext : IContext {
		[SerializeField] private bool animateOnOpen;
		[ShowIf("animateOnOpen")] [SerializeField] private TweenSettings<float> openAnimSettings;
		[SerializeField] private bool animateOnClose;
		[ShowIf("animateOnClose")] [SerializeField] private TweenSettings<float> closeAnimSettings;
		
		public void StartOpenAnimation(Transform target, Action onAnimCompleteAction = null) {
			target.gameObject.SetActive(true);
			
			if (!animateOnOpen) {
				onAnimCompleteAction?.Invoke();
				return;
			};

			Tween tween = Tween.ScaleY(target, openAnimSettings);
			if (onAnimCompleteAction != null) tween.OnComplete(onAnimCompleteAction);
		}
		
		public void StartCloseAnimation(Transform target, Action onAnimCompleteAction = null) {
			if (!animateOnClose) {
				onAnimCompleteAction?.Invoke();
				return;
			}
			
			Tween tween = Tween.ScaleY(target, closeAnimSettings);
			if (onAnimCompleteAction != null) tween.OnComplete(onAnimCompleteAction);
		}
	}
}