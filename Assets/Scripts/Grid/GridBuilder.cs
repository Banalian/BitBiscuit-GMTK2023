using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Serialization;

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
        [Tooltip("Size of the grid, \nX: number of columns on wide rows\nY: number of rows")]
        private Vector2 gridSize = new Vector2(3f,3f);
        
        [SerializeField] 
        [Tooltip("The object that will be used to build the grid")]
        private GameObject buildingBlock;
        

        private void Start()
        {
            BuildGrid();
        }

        private void BuildGrid()
        {
            Vector2 currentPosition = this.transform.position;
            for (int i = 0; i < gridSize.y; i++)
            {
                for (int j = 0; j < gridSize.x + (i % 2 == 0 ? 0 : 1); j++)
                {
                    var newBrick = Instantiate(buildingBlock,currentPosition,Quaternion.identity,this.transform);
                    newBrick.name += $" {i},{j}";
                    currentPosition.x += spacing.x + brickSize.x;
                }

                currentPosition.x = this.transform.position.x;
                currentPosition.y -= spacing.y + brickSize.y;
                currentPosition.x -= (i % 2 == 0 ? 1 : 0) * (brickSize.x+spacing.x) / 2;
            }
        }
    }
}
