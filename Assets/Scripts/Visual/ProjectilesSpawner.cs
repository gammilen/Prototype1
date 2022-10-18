using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Visual
{
    public class ProjectilesSpawner : MonoBehaviour
    {
        [SerializeField] private ProjectilesLauncher _launcher;
        [SerializeField] private Transform _root;

        private void OnEnable()
        {
            _launcher.NewStateEvent += SpawnNewProjectile;
        }

        private void OnDisable()
        {
            _launcher.NewStateEvent -= SpawnNewProjectile;
            StopAllCoroutines();
        }

        private void Start()
        {
            var projectiles = new HashSet<Projectile>();
            foreach (var proj in _launcher.Projectiles)
            {
                var prefab = proj.Data.Prefab;
                var projObj = Instantiate(prefab, _root).GetComponent<Projectile>();
                var component = _launcher.GetProjectileComponent(proj);
                projObj.gameObject.SetActive(false);
                projObj.Init(component);
                projectiles.Add(projObj);
            }
        }

        private void SpawnNewProjectile(ProjectileState state)
        {
            StartCoroutine(Spawn(state));
        }

        private IEnumerator Spawn(ProjectileState state)
        {
            var prefab = state.Data.Prefab;
            yield return prefab;
            var projObj = Instantiate(prefab, _root).GetComponent<Projectile>();
            var component = _launcher.GetProjectileComponent(state);
            projObj.Init(component);
        }
    }
}