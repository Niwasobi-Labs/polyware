using PolyWare.Core.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PolyWare.Audio {
	public class ButtonSounds : MonoBehaviour, ISelectHandler, ISubmitHandler {
		public AudioClip SelectSound;
		public AudioClip SubmitSound;
		
		public void OnSelect(BaseEventData eventData) {
			ServiceLocator.Global.Get<IAudioService>().PlaySfx(SelectSound, transform.position, AudioChannel.UISfx);
		}

		public void OnSubmit(BaseEventData eventData) {
			ServiceLocator.Global.Get<IAudioService>().PlaySfx(SubmitSound, transform.position, AudioChannel.UISfx);
		}
	}    
}

