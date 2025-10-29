using System;
using System.Collections.Generic;

namespace PolyWare.Stats {
	public class StatModifierManager {
		private readonly LinkedList<StatModifier> modifiers = new LinkedList<StatModifier>();
		
		public event EventHandler<StatQuery> Queries;
		
		public void PerformQuery(object sender, StatQuery query) => Queries?.Invoke(sender, query);
		
		public void AddModifier(StatModifier modifier) {
			modifiers.AddLast(modifier);
			Queries += modifier.Handle;

			modifier.OnDispose += _ => RemoveModifier(modifier);
		}

		public void RemoveModifier(StatModifier modifier) {
			modifiers.Remove(modifier);
			Queries -= modifier.Handle;
		}
		
		public void Update(float deltaTime) {
			var node = modifiers.First;
			while (node != null) {
				StatModifier modifier = node.Value;
				modifier.Update(deltaTime);
				node = node.Next;
			}
			
			node = modifiers.First;
			while (node != null) {
				var nextNode = node.Next;

				if (node.Value.MarkedForRemoval) {
					node.Value.Dispose();
				}
				
				node = nextNode;
			}
		}
	}
}
