namespace PolyWare.Entities {
	public interface IEntityOverrideData {}
	
	public interface IAllowSpawnOverride {
		public void Override(IEntityOverrideData overrideData);
	}
}