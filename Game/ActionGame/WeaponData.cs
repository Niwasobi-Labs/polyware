using System;
using PolyWare.Core.Entities;
using UnityEngine.Events;

namespace PolyWare.ActionGame {
	[Serializable]
	public class WeaponSpawnData : IEntitySpawnData {
		//public int StartingCondition;
	}
	
	[Serializable]
	public abstract class WeaponData : EntityData<EquipmentDefinition>, IAllowSpawnOverride {
		//public float Condition;
		
		public event UnityAction<WeaponData> OnDataChanged = delegate { };
		
		protected WeaponData(EquipmentDefinition definition) : base(definition) { }
		
		public virtual void Override(IEntitySpawnData data) {
			var spawnData = data as WeaponSpawnData;
			//Condition = spawnData.StartingCondition == -1 ? 1 : spawnData.StartingCondition;
		}
		
		protected void RaiseDataChangedEvent() {
			OnDataChanged.Invoke(this);
		}
	}

}