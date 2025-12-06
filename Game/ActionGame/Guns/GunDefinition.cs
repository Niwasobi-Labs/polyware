using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PolyWare.Game {
	[CreateAssetMenu(menuName = "PolyWare/Definitions/Gun", fileName = "New GunDefinition")]
	public class GunDefinition : EquipmentDefinition {
		
		public override IEntityData CreateDefaultInstance() => new GunData(this);
		
		public enum FireMode {
			SemiAuto,
			Auto
		}

		[field: Title("Shooting")]
		[field: SerializeField] public AbilityDefinition OnSuccessfulUseAbility { get; private set; }
		[field: SerializeField] public AbilityDefinition FireAbility { get; private set; }
		[field: SerializeField] public DamageContext Damage { get; private set; }
		[field: SerializeReference] public IDamageEvaluator BulletDamageEvaluator { get; private set; } = new DefaultDamageEvaluator();
		[field: SerializeField] public FireMode FiringMode { get; private set; }
		[field: SerializeField] public ProjectileDefinition Bullet { get; private set; } 
		[field: SerializeField] public EventReference ShootingSound { get; private set; }
		[field: SerializeField] public float FireRate { get; private set; } = 1.0f;
		[field: SerializeReference] public IFireRateEvaluator FireRateEvaluator { get; private set; } = new DefaultFireRateEvaluator();
		[field: SerializeField] public float BulletSpeed { get; private set; } = 50.0f;
		[field: SerializeField] public float Range { get; private set; } = 25.0f;
		[field: SerializeField] public float Spread { get; private set; }
		[field: SerializeField] public float SpreadOffset { get; private set; }
		[field: SerializeField] public bool RandomSpreadVariance { get; private set; } = false;
		[field: SerializeField] public int BulletCountPerShot { get; private set; } = 1;
		
		[field: Title("Ammo")]
		[field: SerializeField] public int MaxAmmo { get; private set; }
		[field: SerializeField] public int MaxReserveAmmo { get; private set; }
		[field: SerializeField] public int AmmoConsumptionPerShot { get; private set; } = 1;
		[field: SerializeField] public bool InfiniteAmmo { get; private set; } = false;
		[field: SerializeField] public bool BottomlessClip { get; private set; } = false;
		
		[field: Title("Reloading")] 
		[field: SerializeField] public ReloadStrategy ReloadType { get; private set; }
		[field: SerializeField] public float ReloadTime { get; private set; } = 2.0f;
		[field: SerializeField] [field: ShowIf("ReloadType", ReloadStrategy.OneByOne)] public int AmmoPerReload { get; private set; } = 1;
		[field: SerializeField] public EventReference ReloadingSfx { get; private set; }

		[field: Title("Heating")] 
		[field: SerializeField] public bool CanOverheat { get; private set; }
		[field: SerializeField] public int MaxHeat { get; private set; }
		[field: SerializeField] public float HeatPerShot { get; private set; }
		[field: SerializeField] public float HeatCooldownRate { get; private set; } = 5f;
		[field: SerializeField] public float OverHeatTime { get; private set; } = 3f;
		
		[field: Title("Aim Assist")] 
		[field: SerializeField] public AimAssistInfo AimAssist { get; private set; }
		[field: SerializeField] public bool UseLaserSight { get; private set; }
		[Tooltip("Percentage of the Gun's Range. eg. 1 = Range, 0.5 = Half")]
		[field: SerializeField] [Range(0, 1)] public float RangeOfLaserSight { get; private set; } = 1;
	}
}