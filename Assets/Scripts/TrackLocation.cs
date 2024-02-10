using UnityEngine;
using UnityEngine.Serialization;

namespace CubeRunner
{
    public class TrackLocation : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("m_LocationTrigger")]
        public PhysicsCallbacks locationTrigger;
    }
}