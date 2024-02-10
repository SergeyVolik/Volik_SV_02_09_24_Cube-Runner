using UnityEngine;

namespace CubeRunner
{
    public class CollectableObject : MonoBehaviour, IVisitor<Player>
    {
        public void Visit(Player toVisit)
        {
            toVisit.AddCube();
            gameObject.SetActive(false);
        }
    }
}