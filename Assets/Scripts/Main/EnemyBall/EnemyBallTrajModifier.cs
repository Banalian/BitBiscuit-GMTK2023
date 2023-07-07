using System;
using System.Collections.Generic;
using Brick;
using UnityEngine;

namespace Main.EnemyBall
{
    /// <summary>
    /// Script that check if we're in range of a ball trajectory modifier, if so, modify the ball's trajectory
    /// </summary>
    public class EnemyBallTrajModifier : MonoBehaviour
    {
        
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            List<BallTrajModifierBrick> trajModifiers = GetTrajModifiers();
            
            InfluenceTrajectory(trajModifiers);
        }

        /// <summary>
        /// Influence the ball's trajectory based on all the traj modifiers in range and activated.
        /// </summary>
        /// <param name="trajModifiers">List of modifier to use</param>
        private void InfluenceTrajectory(List<BallTrajModifierBrick> trajModifiers)
        {
            foreach (BallTrajModifierBrick trajModifier in trajModifiers)
            {
                // get the direction from the ball to the center of the traj modifier
                Vector2 direction = (trajModifier.transform.position - transform.position).normalized;
                
                // get the force to apply to the ball
                Vector2 force = direction * trajModifier.force;
                
                // apply the force to the ball
                _rb.velocity += force;
            }
        }

        /// <summary>
        /// Gather all the traj modifiers in range of the ball and activated.
        /// </summary>
        /// <returns>A list containing all the trajectory modifier bricks that are active and able to influence the ball</returns>
        private List<BallTrajModifierBrick> GetTrajModifiers()
        {
            // get a list of the traj modifier zones, and check if we're in range of one of them
            BallTrajModifierBrick[] trajModifierBricks = FindObjectsOfType<BallTrajModifierBrick>();

            List<BallTrajModifierBrick> trajModifierBricksInRange = new List<BallTrajModifierBrick>();

            foreach (BallTrajModifierBrick trajModifierBrick in trajModifierBricks)
            {
                // if we're in range of a traj modifier zone, add it to the list to modify the ball's trajectory
                if (trajModifierBrick.IsInRange(transform.position) && trajModifierBrick.IsActivated)
                {
                    trajModifierBricksInRange.Add(trajModifierBrick);
                }
            }
            
            return trajModifierBricksInRange;
        }
    }
}