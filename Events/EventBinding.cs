using System;

// based on https://github.com/adammyhre/Unity-Event-Bus
namespace PolyWare.Events {
    public interface IEventBinding<T> {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }

    public class EventBinding<T> : IEventBinding<T> where T : IEvent {
        Action<T> onEvent = _ => { };
        Action onEventNoArgs = () => { };

        Action<T> IEventBinding<T>.OnEvent {
            get => onEvent;
            set => onEvent = value;
        }

        Action IEventBinding<T>.OnEventNoArgs {
            get => onEventNoArgs;
            set => onEventNoArgs = value;
        }

        public EventBinding(Action<T> onEvent) => this.onEvent = onEvent;
        public EventBinding(Action onEventNoArgs) => this.onEventNoArgs = onEventNoArgs;

        public void Add(Action newOnEvent) => onEventNoArgs += newOnEvent;
        public void Remove(Action newOnEvent) => onEventNoArgs -= newOnEvent;

        public void Add(Action<T> newOnEvent) => this.onEvent += newOnEvent;
        public void Remove(Action<T> newOnEvent) => this.onEvent -= newOnEvent;
    }
}