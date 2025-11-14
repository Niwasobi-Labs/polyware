using PolyWare.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace PolyWare.Game {
	public class SpawnEntity : MonoBehaviour {
		[SerializeField] private UnityMonoEvents whenToSpawn = UnityMonoEvents.Manual;
		[SerializeField] private EntityDefinition entity;
		[SerializeField] private int destroyAfterSpawning = 1;

		[SerializeField] [HideIf("whenToSpawn", UnityMonoEvents.OnDestroy)]
		private float spawnDelay = 0;

		public UnityEvent<GameObject> OnSpawn;

		private CountdownTimer spawnTimer;
		private int spawnCount;

		private void Awake() {
			spawnTimer = new CountdownTimer(spawnDelay);
			spawnTimer.OnTimerComplete += () => Spawn();
			
			if (whenToSpawn == UnityMonoEvents.OnAwake) spawnTimer.Start();
		}
		
		private void OnEnable() {
			if (whenToSpawn == UnityMonoEvents.OnEnable) spawnTimer.Start();
		}
		
		private void Start() {
			if (whenToSpawn == UnityMonoEvents.OnStart) spawnTimer.Start();
		}

		private void Update() {
			spawnTimer.Tick(Time.deltaTime);
			if (whenToSpawn == UnityMonoEvents.OnUpdate && !spawnTimer.IsRunning) spawnTimer.Start();
		}

		private void OnDisable() {
			if (whenToSpawn == UnityMonoEvents.OnDisable) spawnTimer.Start();
		}

		private void OnDestroy() {
			if (whenToSpawn == UnityMonoEvents.OnDestroy && spawnCount < destroyAfterSpawning) spawnTimer.Start();
		}

		public IEntity Spawn() {
			if (!entity) return null;
			
			IEntity newEntity = EntityFactory<IEntity>.CreateFrom(entity, transform.position, transform.rotation);
			OnSpawn?.Invoke(newEntity.GameObject);

			spawnCount++;
			if (destroyAfterSpawning > 0 && spawnCount >= destroyAfterSpawning) Destroy(gameObject);
			return newEntity;
		}
	}
}