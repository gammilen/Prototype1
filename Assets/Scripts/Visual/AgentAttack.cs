using System.Collections;
using UnityEngine;
using Game;

namespace Visual
{
    public class AgentAttack : MonoBehaviour
    {
        private IAttackComponent _attackComponent;

        private void OnEnable()
        {
            if (_attackComponent != null)
            {
                _attackComponent.Destroyed += HandleDestroy;
            }
        }

        private void OnDisable()
        {
            if (_attackComponent != null)
            {
                _attackComponent.Destroyed -= HandleDestroy;
            }
        }

        private void Update()
        {
            if (_attackComponent != null && Time.deltaTime != 0)
            {
                _attackComponent.Attack(Time.deltaTime);
            }
        }

        public void Init(IAttackComponent attackComponent)
        {
            _attackComponent = attackComponent;
            _attackComponent.Destroyed += HandleDestroy;
        }

        private void HandleDestroy()
        {
            Destroy(gameObject);
        }
    }
}