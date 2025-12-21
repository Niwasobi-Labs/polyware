using System;
using UnityEngine;

namespace PolyWare.Core
{
	[RequireComponent(typeof(RectTransform))]
	public class Widget : MonoBehaviour
	{
		[SerializeField] private GameObject visualParentOverride;
		[SerializeField] protected WidgetTweenAnimationContext tweenAnimationContext;

		public event Action OnCloseAnimationComplete;

		protected GameObject VisualParent => visualParentOverride ? visualParentOverride : gameObject;

		public virtual void Open()
		{
			tweenAnimationContext.StartOpenAnimation(VisualParent.transform);
		}

		public virtual void Close()
		{
			tweenAnimationContext.StartCloseAnimation(VisualParent.transform, () =>
			{
				VisualParent.SetActive(false);
				OnCloseAnimationComplete?.Invoke();
			});
		}
	}
}