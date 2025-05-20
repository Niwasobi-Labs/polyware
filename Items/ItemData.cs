using PolyWare.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Items {
	public abstract class ItemData : EntityData {
		[field: Title("Item")]
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public AudioClip PickupSound { get; private set; }
	}
}