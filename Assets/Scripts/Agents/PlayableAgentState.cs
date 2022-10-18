using System.Collections;
using UnityEngine;
using Newtonsoft.Json;

namespace Game
{
    public interface IPlayableAgent
    {
        
    }

    public interface IHealthState
    {
        int Health { get; set; }
    }

    public interface IEscapeState
    {
        bool IsEscaped { get; }
    }

    [System.Serializable]
    public class PlayableAgentState : IMoveState, IPlayableAgent, IHealthState, IEscapeState
    {
        [JsonConverter(typeof(SOConverter<Agent>))]
        public Agent Data { get; private set; } // todo: to interface
        public int Health { get; set; }
        public float Speed { get; private set; }
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Position { get; set; }
        public bool IsActive { get; private set; }
        public bool IsEscaped { get; private set; }

        public PlayableAgentState(PlayableAgentData data)
        {
            Data = data;
            Health = data.Health;
            Speed = data.Speed;
        }

        [JsonConstructor]
        public PlayableAgentState(Agent data, int health, float speed, Vector3 position, bool isActive, bool isEscaped)
        {
            Data = data;
            Health = health;
            Speed = speed;
            Position = position;
            IsActive = isActive;
            IsEscaped = isEscaped;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void SetEscaped()
        {
            IsEscaped = true;
        }
    }
}