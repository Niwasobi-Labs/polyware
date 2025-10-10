using System;
using System.Collections.Generic;

namespace PolyWare.Core.Services {
	public class ServiceManager {
		private Dictionary<Type, IService> services = new Dictionary<Type, IService>();
		
		public void Register<T>(IService service) where T : IService {
			if (!Has<T>()) {
				services.Add(typeof(T), service);
				return;
			}
			
			services[typeof(T)] = service;	
		}

		public T Get<T>() where T : IService {
			if (services.TryGetValue(typeof(T), out IService s)) return (T)s;
			throw new Exception($"Service {typeof(T).Name} not registered");
		}

		public bool Has<T>() where T : IService {
			return services.ContainsKey(typeof(T));
		}
		
		public void Clear() {
			services.Clear();
		}
	}
}