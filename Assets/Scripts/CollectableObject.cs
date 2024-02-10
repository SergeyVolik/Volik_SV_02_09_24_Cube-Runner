using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    public void Visit(Player toVisit)
    {
        toVisit.AddCube();
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.TryGetComponent<Player>(out var playerC))
        {
            Visit(playerC);
        }
    }
}
