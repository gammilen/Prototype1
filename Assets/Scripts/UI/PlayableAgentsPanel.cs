using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI
{
    public class PlayableAgentsPanel : MonoBehaviour
    {
        [SerializeField] private LevelAgentsRegistry _agents;
        [SerializeField] private SwitchActiveAgent _switch;
        [SerializeField] private Button _template;
        [SerializeField] private RectTransform _root;

        private void Start()
        {
            for (int i = 0; i < _agents.PlayableAgents.Count; i++)
            {
                PlayableAgentState a = _agents.PlayableAgents[i];
                var item = Instantiate(_template, _root);
                item.GetComponentInChildren<Text>().text = (i+1).ToString();
                item.onClick.AddListener(() => _switch.Switch(a));
            }

        }
    }
}