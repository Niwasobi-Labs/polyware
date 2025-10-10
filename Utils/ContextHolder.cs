using System;
using System.Collections.Generic;
using PolyWare.Debug;

namespace PolyWare.Utils {
	public interface IContext { }
	
	public class ContextHolder {
		protected readonly Dictionary<Type, IContext> contexts = new Dictionary<Type, IContext>();
		
		public ContextHolder(List<IContext> initialContexts = null) {
			if (initialContexts == null) return;
			
			foreach (var context in initialContexts) {
				Add(context);
			}
		}
		
		public void Add(IContext context) {
			if (!contexts.TryAdd(context.GetType(), context)) {
				Log.Error($"Context {context.GetType()} already exists");
			}
		}

		public void Set(IContext context) {
			contexts[context.GetType()] = context;
		}
		
		public T Get<T>() where T : IContext {
			return (T)contexts[typeof(T)];
		}

		public bool TryGet<T>(out T context) where T : IContext {
			if (!contexts.ContainsKey(typeof(T))) {
				context = default;
				return false;
			}
			context = (T)contexts[typeof(T)];
			return true;
		}
		
		public void Remove(IContext context) {
			contexts.Remove(context.GetType());
		}
	}
}