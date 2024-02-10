using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayExecutor
{
    private static MonoExecutor Instance {
        get {
            if (m_Instance == null)
            {
                m_Instance = new GameObject("MonoExecutor").AddComponent<MonoExecutor>();
            }
            return m_Instance;
        }
    }
    private static MonoExecutor m_Instance;
    public static void Execute(float delay, Action action)
    {
        Instance.executionList.Add(new ExecutorData
        {
            action = action,
            duration = delay
        });
    }

    private class ExecutorData
    {
        public Action action;
        public float t;
        public float duration;
    }

    private class MonoExecutor : MonoBehaviour
    {
        public List<ExecutorData> executionList = new List<ExecutorData>();
        private List<ExecutorData> m_ToRemove = new List<ExecutorData>();

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            foreach (var item in executionList)
            {
                item.t += deltaTime;

                if (item.t > item.duration)
                {
                    item.action?.Invoke();
                    m_ToRemove.Add(item);
                }
            }

            foreach (var item in m_ToRemove)
            {
                executionList.Remove(item);
            }

            m_ToRemove.Clear();
        }
    }
}



