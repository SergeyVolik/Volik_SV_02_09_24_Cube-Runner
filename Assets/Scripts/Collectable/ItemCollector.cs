using System;
using UnityEngine;

namespace CubeRunner
{
    public class ItemCollector : MonoBehaviour
    {
        private CubeHealthBar m_HealthBar;
        [SerializeField]
        private WorldSpaceMessage m_WorldSpaceMessagePrefab;

        private void Awake()
        {
            m_HealthBar = GetComponent<CubeHealthBar>();
        }

        public event Action<ICollectable> onCollected = delegate { };
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ICollectable>(out var obj))
            {
                obj.Collect();
                onCollected.Invoke(obj);

                if (obj is AttachCubeCollectable visitor)
                {
                    m_HealthBar.AddCube();
                    var poolMessage = GameObjectPool.GetPoolObject(m_WorldSpaceMessagePrefab);

                    poolMessage.Show(m_HealthBar.GetNextTopPosition(), "+1");
                }
            }
        }
    }
}
