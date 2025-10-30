using PolyWare.Core;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

namespace PolyWare.Game {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "FindRandomLocation", story: "[Agent] finds random [Location] within [Radius] units", category: "Action/Navigation", id: "517bdb0cb6f8dd071eda997a44e821b5")]
    public partial class FindRandomLocationAction : Action {
        [SerializeReference] public BlackboardVariable<GameObject> Agent;
        [SerializeReference] public BlackboardVariable<float> Radius;
        [SerializeReference] public BlackboardVariable<Vector3> Location;

        private NavMeshAgent navMeshAgent;

        private void Initialize() {
            if (!navMeshAgent) navMeshAgent = Agent.Value.GetComponent<NavMeshAgent>();
        }
    
        protected override Status OnStart() {
            Initialize();
        
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * Radius;
            randomDirection += navMeshAgent.transform.position;
            NavMeshHit hit;
            int failures = 0;
            while (failures < 10) {
                if (NavMesh.SamplePosition(randomDirection, out hit, Radius, NavMesh.AllAreas)) {
                    Location.Value = hit.position;
                    return Status.Success;
                }
                failures++;
            }

            Log.Error($"{navMeshAgent.gameObject} could not find new location");
            return Status.Failure;
        }
    }

}

