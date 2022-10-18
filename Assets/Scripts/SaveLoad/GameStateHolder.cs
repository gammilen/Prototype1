using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Game
{
    public class SOContainer
    {
        public ScriptableObject SO;
        public SOContainer(ScriptableObject so)
        {
            SO = so;
        }
    }
    public class SOConverter<T> : JsonConverter<T>
        where T: ScriptableObject, new()
    {
        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            //writer.WriteValue(JsonUtility.ToJson(value));

            var holder = new SOContainer(value);
            writer.WriteValue(JsonUtility.ToJson(holder));
        }

        public override T ReadJson(JsonReader reader, System.Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;

            var t = new T();
            var holder = (SOContainer) JsonUtility.FromJson(s, typeof(SOContainer));
            //var json = JsonConvert.DeserializeAnonymousType(s, holder);

            //JsonUtility.FromJsonOverwrite(holder.SO, t);
            return holder.SO as T;
        }
    }

    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            writer.WriteValue(JsonConvert.SerializeObject(new float[] { value.x, value.y, value.z}));
        }

        public override Vector3 ReadJson(JsonReader reader, System.Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            float[] values = (float[])(JsonConvert.DeserializeObject((string)reader.Value, typeof(float[])));
            return new Vector3(values[0], values[1], values[2]);
        }
    }

    public class Vector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            writer.WriteValue(JsonConvert.SerializeObject(new float[] { value.x, value.y }));
        }

        public override Vector2 ReadJson(JsonReader reader, System.Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            float[] values = (float[])(JsonConvert.DeserializeObject((string)reader.Value, typeof(float[])));
            return new Vector2(values[0], values[1]);
        }
    }

    public class Vector3ListConverter : JsonConverter<IList<Vector3>>
    {
        public override void WriteJson(JsonWriter writer, IList<Vector3> value, JsonSerializer serializer)
        {
            writer.WriteValue(JsonConvert.SerializeObject(value.Select(x => new Vector3Info(x))));
        }

        public override IList<Vector3> ReadJson(JsonReader reader, System.Type objectType, IList<Vector3> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            IList<Vector3> values = (IList<Vector3>)(JsonConvert.DeserializeObject((string)reader.Value, typeof(IList<Vector3>)));
            return values;
        }
    }

    [CreateAssetMenu(fileName ="GameState", menuName ="Components/Game State Holder")]
    public class GameStateHolder : ScriptableObject
    {
        private class State
        {
            public string[] Data;
            public State(List<string> data)
            {
                Data = data.ToArray();
            }
        }

        [SerializeField] private List<StateHolderScriptableObject> _stateHolders;

        public void SetStateData(string data)
        {
            var sData = JsonConvert.DeserializeObject<State>(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            var statesData = sData as State;
            //var statesData = JsonUtility.FromJson<State>(data);
            for (int i = 0; i < _stateHolders.Count; i++)
            {
                
                var obj = JsonConvert.DeserializeObject(statesData.Data[i], _stateHolders[i].GetType(),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                   });
                _stateHolders[i].SetData(obj);

                //JsonUtility.FromJsonOverwrite(statesData.Data[i], _stateHolders[i]);
            }
        }

        public string GetData()
        {
            /*return JsonConvert.SerializeObject(_stateHolders, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });*/
            var statesData = new List<string>();
            for (int i = 0; i < _stateHolders.Count; i++)
            {
                statesData.Add(JsonConvert.SerializeObject(_stateHolders[i], new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                }));
                //JsonUtility.ToJson(_stateHolders[i]));
            }

            return JsonConvert.SerializeObject(new State(statesData), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }

    }
}