using UnityEngine;

namespace Brick
{
    public class MultiplierBoostableBrick : MonoBehaviour, IBoostableBrick
    {
        [SerializeField] 
        private float multiplierUpgrade = .1f;
        
        public void Boost()
        {
            PointBonusBrick bonusBrick = GetComponent<PointBonusBrick>();
            
            if(!bonusBrick) return;
            bonusBrick.UpgradeMultiplier(multiplierUpgrade);
        }
    }
}