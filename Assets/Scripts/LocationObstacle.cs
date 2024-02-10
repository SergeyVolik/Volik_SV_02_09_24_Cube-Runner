using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using CandyCoded.HapticFeedback;

namespace CubeRunner
{
    public class LocationObstacle : MonoBehaviour, IVisitor<CubeHealthBar>
    {
        private CinemachineImpulseSource m_CameraImpuls;
        IEnumerable<Collider> m_WallBlocksColliders;

        private void Awake()
        {
            m_CameraImpuls = GetComponent<CinemachineImpulseSource>();

            m_WallBlocksColliders = gameObject
                .GetComponentsInChildren<Collider>()
                .Where((e) => e.gameObject != gameObject);
        }

        public void Visit(CubeHealthBar player)
        {
            var healthCubes = player.GetHealthCubes().ToArray();

            bool executeDeath = false;

            Handheld.Vibrate();

            foreach (var block in m_WallBlocksColliders)
            {
                if (false == block.gameObject.activeSelf)
                {
                    continue;
                }

                Vector3 blockPos = block.transform.position;
                Bounds blockBounds = block.bounds;

                foreach (var healthCube in healthCubes)
                {
                    Vector3 healkthCubePos = healthCube.transform.position;

                    if (false == healthCube.Collider.bounds.Intersects(blockBounds))
                    {
                        continue;
                    }

                    if (player.IsTopCube(healthCube))
                    {
                        executeDeath = true;
                    }

                    player.RemoveCube(healthCube);
                    healkthCubePos.z = blockPos.z - Constants.CUBE_SIZE;

                    healthCube.transform.position = healkthCubePos;
                }
            }

            if (executeDeath)
            {
                player.ExecuteDeath();
            }
            else
            {
                HapticFeedback.MediumFeedback();
            }

            m_CameraImpuls.GenerateImpulse();
        }
    }
}