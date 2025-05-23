using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.AssetManagement {
	[CreateAssetMenu(fileName = "SpriteSheetCollection", menuName = "PolyWare/SpriteSheetCollection")]
	public class SpriteSheetCollection : ScriptableObject {
		[FormerlySerializedAs("SpriteSheets")] public TMP_SpriteAsset spriteSheets;
	}
}