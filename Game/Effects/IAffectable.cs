using PolyWare.Utils;
using UnityEngine;

namespace PolyWare.Effects {
	public interface IAffectable {
		public GameObject GameObject { get; }
		public void Affect(IEffect effect, ContextHolder ctx);
	}
}