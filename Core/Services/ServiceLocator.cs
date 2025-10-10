using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace PolyWare.Core.Services {
	public class ServiceLocator {
		private static ServiceLocator _globalServiceLocator;
		private static readonly Dictionary<Scene, ServiceLocator> _sceneLocators = new Dictionary<Scene, ServiceLocator>();

		public static ServiceLocator Global {
			get {
				_globalServiceLocator ??= new ServiceLocator();
				return _globalServiceLocator;
			}
		}


		public static void RemoveSceneLocator(Scene scene) {
			if (!_sceneLocators.TryGetValue(scene, out ServiceLocator locator)) return;
			locator.Clear();
			_sceneLocators.Remove(scene);
		}
		
		public static ServiceLocator ForSceneOf(Scene scene) {
			if (_sceneLocators.TryGetValue(scene, out ServiceLocator serviceLocator)) {
				return serviceLocator;
			}
			
			serviceLocator = new ServiceLocator();
			_sceneLocators.Add(scene, serviceLocator);
			return serviceLocator;
		} 
		
		private readonly ServiceManager manager = new();

		public void Register<T>(IService service) where T : IService {
			manager.Register<T>(service);
		}

		public T Get<T>() where T : IService {
			return manager.Get<T>();
		}

		public bool Has<T>() where T : IService {
			return manager.Has<T>();
		}
		
		public void Clear() {
			manager.Clear();
		}
	}
}