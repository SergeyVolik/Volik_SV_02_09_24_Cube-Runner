using UnityEngine;

namespace CubeRunner
{
    public class AttachCubeCollectable : MonoBehaviour, ICollectable
    {
        [SerializeField]
        private WorldSpaceMessage m_WorldSpaceMessagePrefab;

        public void Collect()
        {
            gameObject.SetActive(false);
        }
    }
}