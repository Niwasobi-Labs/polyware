using System;
using System.Collections.Generic;
using PolyWare.Debug;

namespace PolyWare.Core {
	public interface IContext { }
	
	public class ContextHolder {
		protected readonly Dictionary<Type, IContext> contexts = new Dictionary<Type, IContext>();

		public ContextHolder() {}
		
		public ContextHolder(List<IContext> contexts) {
			foreach (var context in contexts) {
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
		
		public T Get<T>() where T : class, IContext {
			return contexts[typeof(T)] as T;
		}
		
		public void Remove(IContext context) {
			contexts.Remove(context.GetType());
		}
	}
}