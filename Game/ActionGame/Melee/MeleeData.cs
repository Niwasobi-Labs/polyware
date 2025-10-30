namespace PolyWare.Game {
	public class MeleeData : WeaponData {
		public MeleeData(EquipmentDefinition definition) : base(definition) { }
		
		public MeleeDefinition MeleeDefinition => Definition as MeleeDefinition;
		
	}
}