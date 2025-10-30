using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace PolyWare.Core {
	public struct FMODEventInstance : IAudioEventInstance {
		private EventInstance sound;

		public FMODEventInstance(EventReference reference) {
			sound = RuntimeManager.CreateInstance(reference);
			if (!sound.isValid()) Log.Error("Sound not found, can't create instance");
		}
		
		public void Play() {
			if (!sound.isValid()) return;
			sound.getPlaybackState(out PLAYBACK_STATE playbackState);
			if (playbackState == PLAYBACK_STATE.STOPPED) {
				sound.start();
			}
		}
		
		public void Stop(bool immediately = false) {
			if (!sound.isValid()) return;
			sound.stop(immediately ? STOP_MODE.IMMEDIATE : STOP_MODE.ALLOWFADEOUT);
		}
	}
}