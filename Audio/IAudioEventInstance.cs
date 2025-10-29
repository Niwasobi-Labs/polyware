namespace PolyWare.Audio {
	public interface IAudioEventInstance {
		public void Play();
		public void Stop(bool immediately = false);
	}
}