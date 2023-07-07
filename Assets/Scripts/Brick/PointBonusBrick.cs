using Scoring;
using UnityEngine;

namespace Brick
{
    public class PointBonusBrick : ABrick
    {
        [field:SerializeField] 
        public float BonusMultiplier { get; protected set; } = .1f;

        public override void Initialize()
        {
            base.Initialize();
            
            ScoreManager.Instance.ModifyMultiplier(+BonusMultiplier);
        }

        public override void DestroyBrick(bool manual = false)
        {
            ScoreManager.Instance.ModifyMultiplier(-BonusMultiplier);
            
            base.DestroyBrick(manual);
        }
    }
}