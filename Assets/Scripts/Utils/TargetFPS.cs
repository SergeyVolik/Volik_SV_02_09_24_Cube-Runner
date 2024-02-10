using System;
using UnityEngine;

namespace CubeRunner
{
    public class TargetFPS : MonoBehaviour
    {
        [Range(0, 144)]
        public int fps = 60;
        private void Awake()
        {
            Application.targetFrameRate = fps;
        }
    }
}