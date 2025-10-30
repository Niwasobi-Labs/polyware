namespace PolyWare.Core {
	public interface IAudioEventInstance {
		public void Play();
		public void Stop(bool immediately = false);
	}
}