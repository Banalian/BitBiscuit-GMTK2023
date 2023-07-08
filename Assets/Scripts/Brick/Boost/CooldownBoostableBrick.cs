using UnityEngine;

namespace Brick
{
    public class CooldownBoostableBrick : MonoBehaviour, IBoostableBrick
    {
        public void Boost()
        {
            // try to get the BallTrajModifierBrick since it's the only one with a cd

            BallTrajModifierBrick brick = GetComponent<BallTrajModifierBrick>();
            
            if(!brick) return;

            brick.ReduceCooldown();
        }
    }
}