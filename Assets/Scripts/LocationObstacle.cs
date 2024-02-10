using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CubeRunner
{
    public class LocationObstacle : MonoBehaviour, IVisitor<Player>
    {
        IEnumerable<Collider> wallBlocksColliders;

        private void Awake()
        {
            wallBlocksColliders = gameObject
                .GetComponentsInChildren<Collider>()
                .Where((e) => e.gameObject != gameObject);
        }

        public void Visit(Player player)
        {
            var healthCubes = player.GetHealthCubes().ToArray();

            foreach (var block in wallBlocksColliders)
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

                    player.RemoveCube(healthCube);
                    healkthCubePos.z = blockPos.z - Constants.CUBE_SIZE;

                    healthCube.transform.position = healkthCubePos;
                }
            }
        }
    }
}