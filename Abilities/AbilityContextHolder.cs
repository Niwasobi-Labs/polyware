using PolyWare.Core;

namespace PolyWare.Abilities {
	public class AbilityContextHolder : ContextHolder {
		public AbilityContext AbilityContext;
		
		public AbilityContextHolder(AbilityContext context) {
			AbilityContext = context;
			Add(context);
		}
	}
}