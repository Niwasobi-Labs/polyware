using System.Collections.Generic;

// based on https://github.com/adammyhre/Unity-Event-Bus
namespace PolyWare.Events {
    public static class EventBus<T> where T : IEvent {
        static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public static void Subscribe(EventBinding<T> binding) => bindings.Add(binding);
        public static void Unsubscribe(EventBinding<T> binding) => bindings.Remove(binding);

        public static void Raise(T @event) {
            var snapshot = new HashSet<IEventBinding<T>>(bindings);

            foreach (var binding in snapshot) {
                if (bindings.Contains(binding)) {
                    binding.OnEvent.Invoke(@event);
                    binding.OnEventNoArgs.Invoke();
                }
            }
        }

        static void Clear() {
            Debug.Log.Message($"Clearing {typeof(T).Name} bindings");
            bindings.Clear();
        }
    }
}
