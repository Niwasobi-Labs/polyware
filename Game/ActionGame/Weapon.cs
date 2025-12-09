using System.Collections.Generic;
using PolyWare.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	// todo: remove IUsable and replace with other terminology (maybe specific to subclasses)
	public abstract class Weapon : Entity<WeaponData>, IEquipable, IUsable, IPickupable, IDroppable {
		[InfoBox("Component disabled by default to prevent logic when in world")]
		
		[Title("Weapon")]
		[SerializeField][Required] protected Interaction interaction;
		[SerializeField][Required] private Collider worldCollider;
		[SerializeField][Required] private Rigidbody rigidbdy;
		
		public abstract bool CanUse { get; }

		public override WeaponData Data { get; protected set; }
		
		protected ICharacter myCharacter;
		
		private List<IEffect> equippedEffects = new();
		
		// IItem
		public bool AutoPickup => false;

		public void Pickup(IProximityUser user) {
			EventBus<ItemPickedUpEvent>.Raise(new ItemPickedUpEvent { Position = transform.position, ItemDefinition = Data.Definition });
		}
		
		public void Equip(ICharacter character) {
			enabled = true;
			interaction.gameObject.SetActive(false);
			rigidbdy.isKinematic = true;
			worldCollider.enabled = false;
			myCharacter = character;
			
			ApplyOnEquipEffects();
			
			OnEquip();
		}

		protected abstract void OnEquip();
		
		public bool Unequip() {
			if (!OnUnequip()) return false;
			
			RemoveEquipEffects();
			
			Destroy(gameObject); // todo: consider pooling instead of destroying
			
			return true;
		}
		
		protected abstract bool OnUnequip();
		
		private void ApplyOnEquipEffects() {
			if (Data.Definition.OnEquipEffects.Length == 0) return;
			if (myCharacter == null || !myCharacter.GameObject.TryGetComponent(out IAffectable myCharacterAffectable)) return;
			
			var equipEffects = Data.Definition.OnEquipEffects;
			for (int i = 0; i < equipEffects.Length; ++i) {
				IEffect newEffect = equipEffects[i].Create();
				myCharacterAffectable.Affect(newEffect, null);
				equippedEffects.Add(newEffect);
			}
		}

		private void RemoveEquipEffects() {
			if (equippedEffects.Count == 0) return;
			if (myCharacter == null || !myCharacter.GameObject.TryGetComponent(out IAffectable myCharacterAffectable)) return;
			
			for (int i = 0; i < equippedEffects.Count; i++) {
				myCharacterAffectable.RemoveEffect(equippedEffects[i]);	
			}
			
			equippedEffects.Clear();
		}
		
		public bool Drop() {
			gameObject.SetActive(true);

			enabled = true;
			
			myCharacter = null;
			transform.SetParent(null);

			interaction.gameObject.SetActive(true);
			rigidbdy.isKinematic = false;
			worldCollider.enabled = true;
			
			rigidbdy.AddForce(rigidbdy.gameObject.transform.forward * 100);

			return true;
		}
		
		// IUsable
		public abstract void Use();
		public abstract void StopUsing();
		
		public void NotifyPlayerOfInteraction(bool inRange) {
			if (inRange) {
				EventBus<ItemInRangeEvent>.Raise(new ItemInRangeEvent { ItemTransform = transform, ItemDefinition = Data.Definition });
			}
			else {
				EventBus<ItemInRangeEvent>.Raise(new ItemInRangeEvent { ItemTransform = null, ItemDefinition = null });
			}
		}
	}
}