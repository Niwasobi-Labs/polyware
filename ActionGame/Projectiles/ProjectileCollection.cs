using System;
using PolyWare.AssetManagement;
using UnityEngine;

namespace PolyWare.ActionGame.Projectiles {
	[Serializable]
	public class ProjectileAsset : AssetData<string, ProjectileDefinition> { }

	[CreateAssetMenu(fileName = "New Projectile Collection", menuName = "PolyWare/Collections/Projectile Collection")]
	public class ProjectileCollection : AssetCollection<string, ProjectileAsset, ProjectileDefinition> { }
}