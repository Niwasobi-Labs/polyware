using System;
using PolyWare.AssetManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.ActionGame.Projectiles {
	[Serializable]
	public struct ProjectileAssetInfo {
		[FormerlySerializedAs("prefab")] public Projectile Prefab;
		// [FormerlySerializedAs("display")] public ProjectileDisplay Display; task: replace this with a different collection mapping (https://app.clickup.com/t/86b6j5z5c)
	}

	[Serializable]
	public class ProjectileAsset : AssetData<string, ProjectileAssetInfo> { }

	[CreateAssetMenu(fileName = "ProjectileIconCollection", menuName = "PolyWare/Collections/Projectile Icons")]
	public class ProjectileCollection : AssetCollection<string, ProjectileAsset, ProjectileAssetInfo> { }
}