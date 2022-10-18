using System.Collections;
using UnityEngine;

namespace Game
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private PlayableAgentActivator _activator;
        
        private void Update()
        {
            transform.position = _activator.ActiveAgent.Position + _offset;
        }
    }
}