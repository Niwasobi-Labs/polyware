using PolyWare.Core;
using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class ProjectileAsset : AssetData<string, ProjectileDefinition> { }

	[CreateAssetMenu(fileName = "New Projectile Collection", menuName = "PolyWare/Collections/Projectile Collection")]
	public class ProjectileCollection : AssetCollection<string, ProjectileAsset, ProjectileDefinition> { }
}