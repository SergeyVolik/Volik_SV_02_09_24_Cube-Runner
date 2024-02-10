using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeRunner
{
    public class GameObjectPool
    {
        private static Dictionary<GameObject, GameObjectPool> Pools = new Dictionary<GameObject, GameObjectPool>();
        public GameObject prefab;
        private Stack<GameObject> poolItems = new Stack<GameObject>();

        private Transform m_PoolRoot;
        private Transform PoolRoot
        {
            get
            {
                if (m_PoolRoot == null)
                {
                    var go = new GameObject($"pool: {prefab.name}");

                    m_PoolRoot = go.GetComponent<Transform>();
                }

                return m_PoolRoot;
            }
        }

        public static T GetPoolObject<T>(T prefab) where T : Component
        {
            var obj = GetPoolObject(prefab.gameObject);

            return obj.GetComponent<T>();
        }

        public static GameObject GetPoolObject(GameObject prefab)
        {
            if (Pools.TryGetValue(prefab, out var pool))
            {
                return pool.GetItem();
            }

            pool = new GameObjectPool();

            pool.prefab = prefab;
            Pools.Add(prefab, pool);
            return pool.GetItem();
        }

        GameObject GetItem()
        {
            while (poolItems.Count > 0)
            {
                var item = poolItems.Pop();
                if (item != null)
                {
                    return item;
                }
            }

            PrepareItems(1);

            return poolItems.Pop();
        }

        public void PrepareItems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var item = GameObject.Instantiate(prefab);
                item.SetActive(false);
                var poolRef = item.AddComponent<GOPoolRef>();

                poolRef.pollRef = this;
                poolRef.onDisabled += () =>
                {
                    Release(item);
                };
                item.transform.SetParent(PoolRoot);

                poolItems.Push(item);
            }         
        }

        public void Release(GameObject item)
        {
            poolItems.Push(item);
            item.SetActive(false);
        }
    }

    public class GOPoolRef : MonoBehaviour
    {
        public event Action onDisabled = delegate { };
        public GameObjectPool pollRef;
        private void OnDisable()
        {
            onDisabled.Invoke();
        }
    }
}

