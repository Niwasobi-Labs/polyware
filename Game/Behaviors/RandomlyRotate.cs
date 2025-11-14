using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	public class RandomlyRotate : MonoBehaviour {
		[SerializeField] private GameObject targetCharacter;
		[SerializeField] private GameObject rotationTarget;
		
		private Vector3 rotationAxis;
		private ICharacter character;
		
		private void Awake() {
			rotationAxis = Random.rotation.eulerAngles;
			if (!targetCharacter.TryGetComponent(out character)) {
				Log.Error("Character needed to adjust rotation speed");
			}
		}

		public void Update() {
			if (!rotationTarget) return;
			
			rotationTarget.transform.Rotate(rotationAxis, character.MoveSettings.TurnSpeed * Time.deltaTime);
		}
	}
}