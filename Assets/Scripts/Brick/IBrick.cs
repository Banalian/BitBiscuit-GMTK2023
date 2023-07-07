using UnityEngine;

namespace Brick {

    /// <summary>
    /// Global Interface for the brick. Define what functions a brick should have
    /// </summary>
    public interface IBrick
    {
        /// <summary>
        /// Initialize the brick (set the initial values)
        /// </summary>
        void Initialize();
        
        /// <summary>
        /// What to do when the brick takes a hit
        /// </summary>
        void Hit(GameObject pad, int damage);

        /// <summary>
        /// What to do when the brick is destroyed.
        /// </summary>
        /// <param name="manual"> if true, means it's destroyed by the player (so for example we don't give points, etc</param>
        void Destroy(bool manual = false);

        /// <summary>
        /// Get the score value of the brick
        /// </summary>
        /// <returns>score value of the brick</returns>
        int GetScoreValue();
    }
}

