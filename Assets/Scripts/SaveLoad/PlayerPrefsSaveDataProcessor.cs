using UnityEditor;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(fileName = "TextSaveDateProcessor", menuName = "Services/Save/PlayerPrefs")]
    public class PlayerPrefsSaveDataProcessor : SaveDataProcessor
    {
        private const string NAME = "Save";

        public override void Write(string data, int number)
        {
            PlayerPrefs.SetString(NAME + number, data);
        }

        public override string Read(int number)
        {
            return PlayerPrefs.GetString(GetPath(number));
        }

        public override bool HasData(int number)
        {
            return Read(number).Length > 0;
        }

        private string GetPath(int number)
        {
            return NAME + number;
        }
    }
}