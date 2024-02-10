using UnityEngine;

namespace CubeRunner
{
    public class HealthCube : MonoBehaviour
    {
        public Rigidbody RB { get; private set; }
        public Collider Collider { get; private set; }

        private void Awake()
        {
            Collider = GetComponent<Collider>();
            RB = GetComponent<Rigidbody>();
            RB.WakeUp();
        }       
    }
}