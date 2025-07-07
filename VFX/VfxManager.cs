using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.VFX {
	public class VFXManager : MonoBehaviour {
		private readonly Queue<GameObject> vfxPool = new();
	}
}