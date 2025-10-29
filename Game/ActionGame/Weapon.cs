using PolyWare.Characters;
using PolyWare.Core.Entities;
using PolyWare.Interactions;
using PolyWare.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.ActionGame {
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

		// IItem
		public bool AutoPickup => false;

		public void Pickup(IProximityUser user) {
			Destroy(gameObject);
		}
		
		public void Equip(ICharacter character) {
			enabled = true;
			interaction.gameObject.SetActive(false);
			rigidbdy.isKinematic = true;
			worldCollider.enabled = false;
			myCharacter = character;
			
			OnEquip();
		}

		protected abstract void OnEquip();
		
		public bool Unequip() {
			if (!OnUnequip()) return false;
			
			Destroy(gameObject); // todo: consider pooling instead of destroying
			
			return true;
		}
		
		protected abstract bool OnUnequip();
		
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
	}
}