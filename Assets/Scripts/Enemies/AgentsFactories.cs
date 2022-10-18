using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IAgentFactory
    {
        bool CanProcessData<T>(T data) where T : Agent;
        bool CanProcessState<T>(T data) where T : IAutonomousAgentState;
        IAutonomousAgentState CreateAgentState<T>(T data, Vector3 startPosition, IList<Vector3> trajectory) where T : Agent;
        IMoveComponent CreateMoveComponent<T>(T data) where T : IAutonomousAgentState;
        IAttackComponent CreateAttackComponent<T>(T data) where T : IAutonomousAgentState;
    }

    [CreateAssetMenu(fileName ="AgentsFactories", menuName ="Components/Factories/Agents Factories")]
    public class AgentsFactories : ScriptableObject
    {
        [SerializeField] private List<EnemyLogic> _factories;

        public IAgentFactory GetFactory<T>(T data) where T : Agent
        {
            foreach (var factory in _factories)
            {
                if (factory is IAgentFactory agentFactory &&
                    agentFactory.CanProcessData(data))
                {
                    return agentFactory;
                }
            }
            return null;
        }

        // TODO: remove
        public IAgentFactory GetFactoryForState<T>(T data) where T : IAutonomousAgentState
        {
            foreach (var factory in _factories)
            {
                if (factory is IAgentFactory agentFactory &&
                    agentFactory.CanProcessState(data))
                {
                    return agentFactory;
                }
            }
            return null;
        }
    }
}