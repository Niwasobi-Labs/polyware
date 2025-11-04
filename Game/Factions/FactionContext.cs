using System;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public struct FactionContext : IContext {
		[field: SerializeField] public int ID;
		[field: SerializeField] public bool AllowFriendlyFire;
		[field: SerializeField] public bool AbsorbFriendlyHits;
	}
}