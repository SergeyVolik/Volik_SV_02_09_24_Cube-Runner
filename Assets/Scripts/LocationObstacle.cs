using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using CandyCoded.HapticFeedback;

namespace CubeRunner
{
    public class LocationObstacle : MonoBehaviour, IVisitor<CubeCharacter>
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

        HashSet<HealthCube> m_ToRemove = new HashSet<HealthCube>();

        public void Visit(CubeCharacter character)
        {
            m_ToRemove.Clear();
            var healthCubes = character.HealthBar.GetHealthCubes();

            bool executeDeath = false;
            var chracterBounds = character.CharacterBox.bounds;
            Handheld.Vibrate();

            //interate over all blocks and check bounds intersections
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
                    Vector3 healthCubePos = healthCube.transform.position;

                    //check intersection character - block
                    if (blockBounds.Intersects(chracterBounds))
                    {
                        executeDeath = true;
                        break;
                    }

                    if (false == healthCube.Collider.bounds.Intersects(blockBounds))
                    {
                        continue;
                    }

                    if (character.HealthBar.IsTopCube(healthCube))
                    {
                        executeDeath = true;
                        break;
                    }

                    m_ToRemove.Add(healthCube);
                    healthCubePos.z = blockPos.z - Constants.CUBE_SIZE;

                    healthCube.transform.position = healthCubePos;
                }
            }

            if (executeDeath)
            {
                character.HealthBar.ExecuteDeath();
            }
            else
            {
                foreach (var item in m_ToRemove)
                {
                    character.HealthBar.RemoveCube(item);
                }

                HapticFeedback.MediumFeedback();
            }

            m_CameraImpuls.GenerateImpulse();
        }
    }
}