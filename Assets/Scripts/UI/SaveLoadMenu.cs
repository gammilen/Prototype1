using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Services;

namespace UI
{
    public class SaveLoadMenu : MonoBehaviour
    {
        [SerializeField] private Pause _pause;
        [SerializeField] private SaveLoadService _saveLoadService;
        [SerializeField] private Button _closeBtn;
        [SerializeField] private RectTransform _saveElementsRoot;
        [SerializeField] private RectTransform _loadElementsRoot;

        [SerializeField] private List<Button> _saveBtnElements;
        [SerializeField] private List<Button> _loadBtnElements;


        private void Start()
        {
            _closeBtn.onClick.AddListener(() => gameObject.SetActive(false));
        }

        private void OnEnable()
        {
            _pause.StartPause();
            for (int i = 0; i < _loadBtnElements.Count; i++)
            {
                _loadBtnElements[i].interactable = _saveLoadService.HasDataInSlot(i + 1);
            }
        }

        private void OnDisable()
        {
            _pause.FinishPause();
        }
    }
}