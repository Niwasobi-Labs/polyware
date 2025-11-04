namespace PolyWare.Game {
	public interface IFactionMember {
		FactionContext FactionInfo { get; set; }

		public bool CanDamage(IFactionMember other) {
			return other.FactionInfo.ID != FactionInfo.ID || other.FactionInfo.AllowFriendlyFire;
		}
	}
}