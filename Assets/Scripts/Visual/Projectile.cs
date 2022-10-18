using UnityEngine;
using Game;

namespace Visual
{
    public class Projectile : MonoBehaviour
    {
        private ProjectileComponent _projectileComponent;

        private void OnDisable()
        {
            if (_projectileComponent != null)
            {
                _projectileComponent.ReleaseEvent -= HandleRelease;
            }
        }

        public void Init(ProjectileComponent projectileComponent)
        {
            _projectileComponent = projectileComponent;
            _projectileComponent.ReleaseEvent += HandleRelease;
        }

        private void Update()
        {
            if (_projectileComponent != null && Time.deltaTime != 0)
            {
                var pos = _projectileComponent.Process(Time.deltaTime);
                transform.position = pos;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.isTrigger)
            {
                _projectileComponent.ProcessCollision();
            }
        }

        private void HandleRelease()
        {
            Destroy(gameObject);
        }
    }
}