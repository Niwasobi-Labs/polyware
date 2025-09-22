using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.Utils {
	[CreateAssetMenu(fileName = "Constants", menuName = "PolyWare/Constants")]
	public class Constants : ScriptableObject {
		[FormerlySerializedAs("DialogueCharacterDelay")] [Header("User Interface")]
		public float dialogueCharacterDelay;

		[FormerlySerializedAs("DialogueSkipMultiplier")] [Range(1, 10)]
		public float dialogueSkipMultiplier;

		public bool UseActiveAiming;
		
		public static Vector2 UnitBottomLeft = new Vector2(0, 0);
		public static Vector2 UnitTopLeft = new Vector2(0, 1);
		public static Vector2 UnitTopRight = new Vector2(1, 1);
		public static Vector2 UnitBottomRight = new Vector2(1, 0);
	}
}