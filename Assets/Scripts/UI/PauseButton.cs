using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Services;

namespace UI
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Pause _pause;
        [SerializeField] private GameObject _pauseObj;
        [SerializeField] private GameObject _unpauseObj;
        [SerializeField] private Toggle _toggle;
        

        private void Start()
        {
            _toggle.onValueChanged.AddListener(HandleToggle);
            HandleToggle(false);
        }

        private void HandleToggle(bool value)
        {
            _pauseObj.SetActive(!value);
            _unpauseObj.SetActive(value);
            if (value)
            {
                _pause.StartPause();
            }
            else
            {
                _pause.FinishPause();
            }
        }
    }
}