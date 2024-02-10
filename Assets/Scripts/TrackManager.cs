using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public List<GameObject> LevelParts;

    const int LEVEL_WIDTH = 30;
    const int AWAKE_SPAWN_LOCATIONS = 3;
    private int m_CurrentOffset = 30;
    public static TrackManager Instance { get; private set; }

    [Header("Location Spawn Tween")]
    [SerializeField]
    private float m_TweenDuration = 2f;
    [SerializeField]
    private Ease m_TweenEaseType = Ease.Linear;
    [SerializeField]
    private float m_SpawnYOffset = -50f;
    private void Awake()
    {
        Init();
        Instance = this;
    }

    private void Init()
    {
        for (int i = 0; i < AWAKE_SPAWN_LOCATIONS; i++)
        {
            SpawnNewLocation();
        }
    }

    public TrackLocation SpawnNewLocation()
    {
        var prefab = LevelParts[Random.Range(0, LevelParts.Count)];
        var spawnPos = new Vector3(0, 0, m_CurrentOffset);
        var instance = GameObject.Instantiate(prefab, spawnPos, Quaternion.identity);
        var trackLocation = instance.GetComponent<TrackLocation>();

        trackLocation.locationTrigger.onTriggerEnter += LocationTrigger_onTriggerEnter;
        trackLocation.locationTrigger.onTriggerExit += LocationTrigger_onTriggerExit;

        m_CurrentOffset += LEVEL_WIDTH;

        return trackLocation;
    }

    public void SpawnNewLocationWithTween()
    {
        var newTrack = SpawnNewLocation();

        var startPosition = newTrack.transform.position;
        var endPosition = startPosition;

        startPosition.y = m_SpawnYOffset;
        newTrack.transform.position = startPosition;

        newTrack.transform
            .DOMove(endPosition, m_TweenDuration)
            .SetEase(m_TweenEaseType);
    }

    private void LocationTrigger_onTriggerExit(Collider obj)
    {

    }

    private void LocationTrigger_onTriggerEnter(Collider obj)
    {
        SpawnNewLocationWithTween();
    }
}
