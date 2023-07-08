using UnityEngine;

namespace Brick
{
    public class BuffBrick : ABrick
    {
        
        [field:SerializeField]
        public float Radius { get; protected set; } = 1f;
        
        public override void Initialize()
        {
            base.Initialize();
            
            BoostBricks();
        }

        private void BoostBricks()
        {
            var bricks = Physics2D.OverlapCircleAll(transform.position, Radius, LayerMask.GetMask("Bricks"));
            foreach (var brick in bricks)
            {
                var boostables = brick.GetComponentsInChildren<IBoostableBrick>();
                foreach (var boostable in boostables)
                {
                    boostable.Boost();
                }
            }
        }
    }
}