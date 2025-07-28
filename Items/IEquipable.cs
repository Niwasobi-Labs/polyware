using PolyWare.Characters;

namespace PolyWare.Items {
	public interface IEquipable {
		void Equip(ICharacter character);
		bool Unequip();
	}
}