using PolyWare.AssetManagement;
using UnityEngine;

namespace PolyWare.Core.SceneManagement {
	[CreateAssetMenu(fileName = "New SceneGroupCollection", menuName = "PolyWare/Collections/Scene Group Collection")]
	public class SceneGroupCollection : StringCollection<SceneGroup> { }
}