using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

namespace Grid
{
    
    public class GridBuilder : MonoBehaviour
    {

        [SerializeField] 
        [Tooltip("2D size of one brick in the grid")]
        private Vector2 brickSize = Vector2.zero;
        
        [SerializeField] 
        [Tooltip("Spacing between the bricks of the grid")]
        private Vector2 spacing = Vector2.zero;
        
        [SerializeField] 
        [Tooltip("Size of the grid, \nX: number of columns on narrow rows\nY: number of rows")]
        private Vector2 gridSize = new Vector2(7,9);
        
        [SerializeField] 
        [Tooltip("Position of the core, it will take up its own slots and the two slots immediately above it")]
        private Vector2 corePosition = new Vector2(3,3);
        
        [SerializeField] 
        [Tooltip("The object that will be used to build the grid")]
        private GameObject buildingBlock;
        
        [SerializeField] 
        [Tooltip("The object that will be used as the core")]
        private GameObject core;
        
        [SerializeField]
        [Tooltip("Offset the core by this amount")]
        private Vector2 coreOffset = Vector2.zero;
        
        private void Start()
        {
            BuildGrid();
        }

        private void BuildGrid()
        {
            Vector2 currentPosition = this.transform.position;
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x + (y % 2 == 0 ? 0 : 1); x++)
                {
                    if ((int)corePosition.x == y && (int)corePosition.y == x)
                    {
                        var coreBrick = Instantiate(core,currentPosition,Quaternion.identity,this.transform);
                        var transformPosition = coreBrick.transform.position;
                        transformPosition.x += coreOffset.x;
                        transformPosition.y += coreOffset.y;
                        coreBrick.transform.position = transformPosition;
                        coreBrick.name += $" {y},{x}";
                    }
                    else
                    {
                        //only if we are not on the spots above the core
                        if (!(x == (int)corePosition.x-1 && y == (int)corePosition.y-1 ) &&
                            !(x == (int)corePosition.x && y == (int)corePosition.y-1 ))
                        {
                            var newBrick = Instantiate(buildingBlock,currentPosition,Quaternion.identity,this.transform);
                            newBrick.name += $" {y},{x}";
                        }
                    }
                    currentPosition.x += spacing.x + brickSize.x;
                }

                currentPosition.x = this.transform.position.x;
                currentPosition.y -= spacing.y + brickSize.y;
                currentPosition.x -= (y % 2 == 0 ? 1 : 0) * (brickSize.x+spacing.x) / 2;
            }
        }
    }
}
