using UnityEngine;

namespace CubeRunner
{
    public class CollectableObject : MonoBehaviour, IVisitor<Player>
    {
        [SerializeField]
        private WorldSpaceMessage m_WorldSpaceMessagePrefab;

        public void Visit(Player toVisit)
        {
            toVisit.AddCube();
            gameObject.SetActive(false);

            var poolMessage = GameObjectPool.GetPoolObject(m_WorldSpaceMessagePrefab);

            poolMessage.Show(toVisit.GetTopPosition(), "+1");
        }
    }
}