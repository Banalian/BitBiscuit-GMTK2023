using Scoring;
using UnityEngine;

namespace Brick
{
    public class PointBonusBrick : ABrick
    {
        [field:SerializeField] 
        public float BonusMultiplier { get; protected set; } = .1f;

        [SerializeField]
        private float maxMultiplier = .5f;

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

        public void UpgradeMultiplier(float multiplierUpgrade)
        {
            float multiplierToApply = multiplierUpgrade;
            BonusMultiplier += multiplierUpgrade;
            if (BonusMultiplier > maxMultiplier)
            {
                // only apply what you can add before reaching the max
                multiplierToApply = maxMultiplier - BonusMultiplier;
                BonusMultiplier = maxMultiplier;
            }
            ScoreManager.Instance.ModifyMultiplier(+multiplierToApply);
        }
    }
}