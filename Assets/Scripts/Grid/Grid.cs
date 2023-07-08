using System;
using UnityEngine;

namespace Grid
{
    /// <summary>
    /// A Grid object contains information about the bricks that form the grid
    /// </summary>
    public class Grid : MonoBehaviour
    {
        [Tooltip("array containing the arrays (lines) of slots on the grid")]
        [field:SerializeField]
        public Replaceable[][] Bricks {get; private set; }

        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}