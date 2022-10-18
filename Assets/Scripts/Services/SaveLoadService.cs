using System;
using UnityEngine;
using Game;

namespace Services
{
    [CreateAssetMenu(fileName ="SaveLoad", menuName ="Services/Save Load")]
    public class SaveLoadService : ScriptableObject
    {
        [field: SerializeField] public int SlotsCount { get; private set; }
        [SerializeField] private SaveDataProcessor _dataProcessor;
        [SerializeField] private GameStateHolder _stateHolder;

        public event Action DataLoaded;

        public bool HasDataInSlot(int number)
        {
            return number > 0 && number <= SlotsCount && _dataProcessor.HasData(number);
        }

        public void LoadData(int number)
        {
            var data = _dataProcessor.Read(number);
            if (data.Length > 0)
            {
                _stateHolder.SetStateData(data);
                DataLoaded?.Invoke();
            }
        }

        public void LoadLevelData()
        {
            LoadData(-1);
        }

        public void SaveData(int number)
        {
            var data = _stateHolder.GetData();
            _dataProcessor.Write(data, number);
        }

        public void SaveLevelData()
        {
            SaveData(-1);
        }
    }
}