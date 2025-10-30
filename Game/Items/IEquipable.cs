namespace PolyWare.Game {
	public interface IEquipable {
		void Equip(ICharacter character);
		bool Unequip();
	}
}