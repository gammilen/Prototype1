using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SaveLoadMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private GameObject _menu; 

        private void Start()
        {
            _btn.onClick.AddListener(OpenMenu);
        }

        private void OpenMenu()
        {
            _menu.SetActive(true);
        }
    }
}