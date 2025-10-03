using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.AssetManagement {
	[CreateAssetMenu(menuName = "PolyWare/Collections/SpriteSheetCollection", fileName = "New SpriteSheetCollection")]
	public class SpriteSheetCollection : ScriptableObject {
		[FormerlySerializedAs("SpriteSheets")] public TMP_SpriteAsset spriteSheets;
	}
}