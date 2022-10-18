using System.IO;
using UnityEngine;

namespace Services
{
    public abstract class SaveDataProcessor : ScriptableObject
    {
        public abstract void Write(string data, int number);
        public abstract string Read(int number);
        public abstract bool HasData(int number);
    }

    

    
}