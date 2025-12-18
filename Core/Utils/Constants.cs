using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Core
{
	public abstract class Constants : ScriptableObject
	{
		[Title("User Interface")]
		[field: SerializeField] public float DialogueCharacterDelay { get; private set; } = 0.1f;
		[Range(1, 10)][field: SerializeField] public float DialogueSkipMultiplier { get; private set; } = 1f;
		public Color PrimaryUserInterfaceColor;
		public Color SecondaryUserInterfaceColor;
		public Color TertiaryUserInterfaceColor;

		[Title("Physics")]
		public static readonly string DamageLayerMaskName = "Damage";
		public static LayerMask DamageLayerMask => LayerMask.GetMask(DamageLayerMaskName);

		[Title("Misc")]
		public static Vector2 UnitBottomLeft = new Vector2(0, 0);
		public static Vector2 UnitTopLeft = new Vector2(0, 1);
		public static Vector2 UnitTopRight = new Vector2(1, 1);
		public static Vector2 UnitBottomRight = new Vector2(1, 0);
	}
}