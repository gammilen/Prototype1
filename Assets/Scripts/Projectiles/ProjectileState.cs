using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class ProjectileState
    {
        [JsonConverter(typeof(SOConverter<ProjectileData>))]
        [field: SerializeField] public ProjectileData Data { get; private set; }
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 OriginPosition { get; private set; }
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Position { get; set; }
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Velocity { get; private set; }
        public float ProgressTime { get; set; }
        public float OriginalTime { get; private set; }
        public int Damage { get; private set; }

        public ProjectileState(ProjectileData data, Vector3 origin, Vector3 velocity, int damage, float time)
        {
            Data = data;
            OriginPosition = Position = origin;
            Velocity = velocity;
            Damage = damage;
            OriginalTime = time;
        }

        [JsonConstructor]
        public ProjectileState(ProjectileData data, Vector3 originPosition, Vector3 position, Vector3 velocity,
            float progressTime, float originalTime, int damage)
        {
            Data = data;
            OriginPosition = originPosition;
            Velocity = velocity;
            Damage = damage;
            Position = position;
            ProgressTime = progressTime;
            OriginalTime = originalTime;
        }
    }
}