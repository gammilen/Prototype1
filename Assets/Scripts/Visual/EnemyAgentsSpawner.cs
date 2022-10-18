using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;


namespace Visual
{
    public class EnemyAgentsSpawner : MonoBehaviour
    {
        [SerializeField] private LevelAgentsRegistry _registry;
        [SerializeField] private AgentsFactories _factories;
        [SerializeField] private LevelPlayArea _playArea;
        [SerializeField] private Transform _root;
        private HashSet<GameObject> _enemies;


        private void Start()
        {
            _enemies = new HashSet<GameObject>();
            foreach (var agent in _registry.EnemyAgents)
            {
                if (agent.IsDead)
                {
                    continue;
                }
                var prefab = agent.Data.Prefab;

                var factory = _factories.GetFactoryForState(agent);

                var agentObj = Instantiate(prefab, _root);
              
                var moveComponent = factory.CreateMoveComponent(agent);
                agentObj.GetComponent<AgentMovement>().Init(moveComponent, _playArea);

                var attackComponent = factory.CreateAttackComponent(agent);
                agentObj.GetComponent<AgentAttack>().Init(attackComponent);

                _enemies.Add(agentObj);
            }
        }
    }
}