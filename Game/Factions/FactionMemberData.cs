using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class FactionMemberData {
		[field: SerializeField] public bool AllowFriendlyFire = false;
		[field: SerializeField] public bool AbsorbFriendlyHits = true;
	}
}