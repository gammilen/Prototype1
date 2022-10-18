using System.Collections;
using UnityEngine;

namespace Game
{
    public class SwitchActiveAgent : MonoBehaviour
    {
        [SerializeField] private PlayableAgentActivator _activator;
        [SerializeField] private SwitchActiveInputReader _input;

        public void Switch(PlayableAgentState agentState)
        {
            _activator.ActivateAgent(agentState);
        }

        private void SwitchToNext()
        {
            _activator.ActivateNext();
        }

        private void Update()
        {
            if (Time.deltaTime != 0 && _input.GetSwitch())
            {
                SwitchToNext();
            }
        }
    }
}