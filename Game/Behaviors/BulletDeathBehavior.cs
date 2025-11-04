using System;
using System.Collections.Generic;
using PolyWare.Core;

namespace PolyWare.Game {
	[Serializable]
	public class BulletDeathBehaviorFactory : IBehaviorFactory {
		public ProjectileDefinition Bullet;
		public float SpawnSpreadAngle;
		public int BulletsToSpawn;
		public float BulletSpeed;
		public DamageContext DamageContext;
		public AbilityDefinition Ability;
		
		public IBehavior Create(ICharacter parent) {
			return new BulletDeathBehavior(parent, this);
		}
	}
	
	public class BulletDeathBehavior : DeathBehavior {
		private readonly ProjectileDefinition bullet;
		private readonly float spawnSpreadAngle;
		private readonly int bulletsToSpawn;
		private readonly float bulletSpeed;
		private readonly DamageContext damageContext;
		private readonly AbilityDefinition ability;
		
		public BulletDeathBehavior(ICharacter character, BulletDeathBehaviorFactory factory) : base(character) {
			bullet = factory.Bullet;
			spawnSpreadAngle = factory.SpawnSpreadAngle;
			bulletsToSpawn = factory.BulletsToSpawn;
			bulletSpeed = factory.BulletSpeed;
			damageContext = factory.DamageContext;
			ability = factory.Ability;
		}

		public override void Start() {
			Complete();
		}
		
		public override void Tick(float dt) {
			// noop
		}
		
		public override void Complete() {
			var newProjectileData = new ProjectileData(
				bullet,
				parent.FactionMember.FactionInfo,
				null,
				bulletSpeed);
				
			var abilityCtxHolder = new AbilityContextHolder(
				ability,
				parent.GameObject,
				null,
				new List<IContext> {
					new DamageContext(damageContext, parent.GameObject, ability), // todo: scale this with stats
					parent.FactionMember.FactionInfo
				});
				
			ProjectileSpawnStrategy.Spawn(new ProjectileSpawnContext(
				newProjectileData,
				bulletsToSpawn,
				parent.Transform.position,
				parent.Transform.forward,
				parent.Transform.up,
				spawnSpreadAngle,
				abilityCtxHolder));
		}
	}
}