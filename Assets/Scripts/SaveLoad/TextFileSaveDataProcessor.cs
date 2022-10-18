using System.IO;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(fileName = "TextSaveDateProcessor", menuName = "Services/Save/Text")]
    public class TextFileSaveDataProcessor : SaveDataProcessor
    {
        private string SavePath => Application.persistentDataPath + "/save/";

        public override void Write(string data, int number)
        {
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
            File.WriteAllText(GetPath(number), data);
        }

        public override string Read(int number)
        {
            var path = GetPath(number);
            return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
        }

        public override bool HasData(int number)
        {
            return File.Exists(GetPath(number));
        }

        private string GetPath(int number)
        {
            return SavePath + $"save{number}.txt";
        }
    }
}