using UnityEngine;

namespace PolyWare.Core
{
	public interface IFillable
	{
		public GameObject GameObject { get; }
		public void SetFillAmount(float newFillAmount);
	}
}