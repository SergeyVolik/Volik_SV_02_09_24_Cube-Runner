using UnityEngine;
using UnityEngine.Serialization;

namespace CubeRunner
{
    public class TrackLocation : MonoBehaviour, IResetable
    {
        [SerializeField]
        [FormerlySerializedAs("m_LocationTrigger")]
        public PhysicsCallbacks locationTrigger;

        private AttachCubeCollectable[] m_Collectables;

        private void Awake()
        {
            m_Collectables = GetComponentsInChildren<AttachCubeCollectable>();
        }

        private void OnDisable()
        {
            ResetObj();
        }

        public void ResetObj()
        {
            foreach (var obj in m_Collectables)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}