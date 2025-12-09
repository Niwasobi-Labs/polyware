using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public interface IAffectable {
		public GameObject GameObject { get; }
		public void Affect(IEffect effect, ContextHolder ctx);
		public void RemoveEffect(IEffect effect);
	}
}