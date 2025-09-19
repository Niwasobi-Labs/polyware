using System;
using PolyWare.AssetManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.ActionGame.Projectiles {
	[Serializable]
	public class ProjectileAsset : AssetData<string, ProjectileDefinition> { }

	[CreateAssetMenu(fileName = "New Projectile Collection", menuName = "PolyWare/Collections/Projectile Collection")]
	public class ProjectileCollection : AssetCollection<string, ProjectileAsset, ProjectileDefinition> { }
}