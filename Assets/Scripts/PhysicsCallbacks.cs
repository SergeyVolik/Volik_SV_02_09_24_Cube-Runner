using System;
using UnityEngine;

namespace CubeRunner
{
    public class PhysicsCallbacks : MonoBehaviour
    {
        public event Action<Collider> onTriggerEnter = delegate { };
        public event Action<Collider> onTriggerExit = delegate { };

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExit.Invoke(other);
        }
    }
}