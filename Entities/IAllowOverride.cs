namespace PolyWare.Entities {
	public interface IEntityOverrideData { }
	
	public interface IAllowOverride {
		public void Override(IEntityOverrideData data);
	}
}