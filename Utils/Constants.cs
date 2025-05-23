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
	}
}