using UnityEngine;

namespace Brick
{
    public class CurrencyBoostableBrick : MonoBehaviour, IBoostableBrick
    {

        [SerializeField] 
        private int currencyUpgrade;
        public void Boost()
        {
            // upgrade the death payout of the brick
            ABrick brick = GetComponent<ABrick>();

            if (!brick) return;
            brick.UpgradeDeathScore(currencyUpgrade);
        }
    }
}