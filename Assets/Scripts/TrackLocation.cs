using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TrackLocation : MonoBehaviour
{
    [SerializeField]
    [FormerlySerializedAs("m_LocationTrigger")]
    public PhysicsCallbacks locationTrigger;

    private void Awake()
    {
      
    }
}
