using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LevelProjectiles", menuName ="Components/Level Projectiles")]
    public class LevelProjectiles : StateHolderScriptableObject
    {
        private List<ProjectileState> _projectiles = new List<ProjectileState>();
        public IList<ProjectileState> Projectiles => _projectiles;

        public override void SetData(object data)
        {
            var state = data as LevelProjectiles;
            _projectiles = new List<ProjectileState>(state.Projectiles);
        }

        public void AddProjectile(ProjectileState state)
        {
            _projectiles.Add(state);
        }

        public void RemoveProjectile(ProjectileState state)
        {
            _projectiles.Remove(state);
        }

        public void ResetState()
        {
            _projectiles.Clear();
        }
    }
}