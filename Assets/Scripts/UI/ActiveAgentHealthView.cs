using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI
{
    public class ActiveAgentHealthView : MonoBehaviour
    {
        [SerializeField] private PlayableAgentActivator _activator;
        [SerializeField] private AgentsHealthChange _agentsHealthChange;
        [SerializeField] private Text _health;

        private void OnEnable()
        {
            _activator.ActiveChangedEvent += HandleChangeActive;
            _agentsHealthChange.ChangeEvent += HandleHealthChange;
            Redraw();
        }

        private void OnDisable()
        {
            _activator.ActiveChangedEvent -= HandleChangeActive;
            _agentsHealthChange.ChangeEvent -= HandleHealthChange;
        }

        private void HandleChangeActive()
        {
            Redraw();
        }

        private void HandleHealthChange(IPlayableAgent agent)
        {
            if (_activator.ActiveAgent == agent)
            {
                Redraw();
            }
        }

        private void Redraw()
        {
            _health.text = _activator.ActiveAgent.Health.ToString();
        }
    }
}