using PolyWare.Core;

namespace PolyWare.ActionGame.Projectiles {
	public class ProjectileContext : IContext {
		public readonly ProjectileData Data;
		
		public ProjectileContext(ProjectileData data) {
			Data = data;
		}
	}
}