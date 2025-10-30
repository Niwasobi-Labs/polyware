using FMODUnity;
using PolyWare.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PolyWare.Game {
	public class ButtonSounds : MonoBehaviour, ISelectHandler, ISubmitHandler {
		public EventReference SelectSound;
		public EventReference SubmitSound;
		
		public void OnSelect(BaseEventData eventData) {
			ServiceLocator.Global.Get<IAudioService>().PlayOneShot(SelectSound, transform.position);
		}

		public void OnSubmit(BaseEventData eventData) {
			ServiceLocator.Global.Get<IAudioService>().PlayOneShot(SubmitSound, transform.position);
		}
	}    
}

