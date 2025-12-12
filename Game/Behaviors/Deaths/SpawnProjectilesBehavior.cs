using System;
using System.Collections.Generic;
using PolyWare.Core;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class SpawnProjectilesBehaviorFactory : BehaviorFactory {
		public ProjectileDefinition Bullet;
		public float SpawnSpreadAngle;
		public float SpawnSpreadAngleOffset;
		public int BulletsToSpawn;
		public float BulletSpeed;
		public DamageContext DamageContext;
		public AbilityDefinition Ability;
		
		public override IBehavior Create(ICharacter parent) {
			return new SpawnProjectilesBehavior(parent, this);
		}
	}
	
	public class SpawnProjectilesBehavior : Behavior {
		private readonly ProjectileDefinition bullet;
		private readonly float spawnSpreadAngle;
		private readonly float spawnSpreadAngleOffset;
		private readonly int bulletsToSpawn;
		private readonly float bulletSpeed;
		private readonly DamageContext damageContext;
		private readonly AbilityDefinition ability;
		
		public SpawnProjectilesBehavior(ICharacter character, SpawnProjectilesBehaviorFactory factory) : base(character, factory) {
			bullet = factory.Bullet;
			spawnSpreadAngle = factory.SpawnSpreadAngle;
			spawnSpreadAngleOffset = factory.SpawnSpreadAngleOffset;
			bulletsToSpawn = factory.BulletsToSpawn;
			bulletSpeed = factory.BulletSpeed;
			damageContext = factory.DamageContext;
			ability = factory.Ability;
		}

		protected override void OnStart() {
			Complete();
		}

		protected override void OnTick(float dt) {
			// no-op
		}

		protected override void OnComplete() {
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
				Vector3.forward,
				Vector3.up,
				spawnSpreadAngle,
				spawnSpreadAngleOffset,
				abilityCtxHolder));
			
			
		}
	}
}