using PolyWare.Entities;
using PolyWare.Interactions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Items {
	public abstract class Item : Entity, IItem {
		[SerializeField] [Required] protected ProximityDetector interaction;
		public abstract ItemData Data { get; }
		
		protected abstract void OnPickup(IProximityUser user);
		public void Pickup(IProximityUser user) {
			interaction.GetGameObject().SetActive(false);
			OnPickup(user);
		}
			
		protected abstract void OnDrop();
		public bool Drop() {
			interaction.GetGameObject().SetActive(true);
			OnDrop();
			return true; // later we may need to block dropping for certain items
		}
		
		public abstract void Use();
		public abstract void CancelUse();
		public abstract bool CanUse { get; }
		public void RemoveFromWorld() {
			Destroy(gameObject);
		}
	}
}