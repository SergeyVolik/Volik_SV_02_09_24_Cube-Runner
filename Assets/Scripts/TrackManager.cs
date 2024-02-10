using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CubeRunner
{
    public class TrackManager : MonoBehaviour
    {
        public List<GameObject> LevelParts;

        private int m_CurrentOffset = Constants.LEVEL_LEN;
        public static TrackManager Instance { get; private set; }

        [SerializeField]
        private int m_AwakeSpawnLocations = 3;

        [Header("Location Spawn Tween")]
        [SerializeField]
        private float m_TweenDuration = 2f;
        [SerializeField]
        private Ease m_TweenEaseType = Ease.Linear;
        [SerializeField]
        private float m_SpawnYOffset = -50f;

        public event Action onLocationFinished = delegate { };

        private void Awake()
        {
            Init();
            Instance = this;
        }

        private void Init()
        {
            for (int i = 0; i < m_AwakeSpawnLocations; i++)
            {
                SpawnNewLocation();
            }
        }

        public TrackLocation SpawnNewLocation()
        {
            var prefab = LevelParts[UnityEngine.Random.Range(0, LevelParts.Count)];

            var spawnPos = new Vector3(0, 0, m_CurrentOffset);
            var instance = GameObjectPool.GetPoolObject(prefab);
            instance.transform.position = spawnPos;
            instance.SetActive(true);

            var trackLocation = instance.GetComponent<TrackLocation>();       
            trackLocation.locationTrigger.onTriggerEnter += LocationTrigger_onTriggerEnter;

            Action<Collider> action = null;
                
            action = (coll) =>
            {
                trackLocation.locationTrigger.onTriggerEnter -= LocationTrigger_onTriggerEnter;
                trackLocation.locationTrigger.onTriggerExit -= action;

                DelayExecutor.Execute(2f, () =>
                {
                    instance.SetActive(false);
                });
            };

            m_CurrentOffset += Constants.LEVEL_LEN;

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

        private void LocationTrigger_onTriggerEnter(Collider obj)
        {
            SpawnNewLocationWithTween();
            onLocationFinished.Invoke();
        }
    }
}