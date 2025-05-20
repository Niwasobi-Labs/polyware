using System.Linq;
using UnityEngine;

namespace PolyWare.Gameplay {
	public interface ISpawnPointStrategy {
		public Transform NextSpawnPoint();
	}
}