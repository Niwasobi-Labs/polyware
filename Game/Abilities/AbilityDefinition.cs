using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	[CreateAssetMenu(fileName = "New AbilityDefinition", menuName = "PolyWare/Ability")]
	public class AbilityDefinition : ScriptableObject {
		[Title("Info")]
		public string Name;
		[TextArea(3, 10)] public string Description;
		public float CastTime = 0f;
		
		[Title("Actions")]
		[SerializeField] public List<AbilityActionData> OnSuccessActions = new List<AbilityActionData>();
		[PropertySpace]
		[SerializeField] public List<AbilityActionData> OnKillActions = new List<AbilityActionData>();
		
		public Ability CreateInstance() => new (this);
	}
}