using System;
using UnityEngine;

namespace Brick
{
    public class HealthBoostableBrick : MonoBehaviour, IBoostableBrick
    {
        
        private HealthManager _healthManager;

        private void Awake()
        {
            _healthManager = GetComponent<HealthManager>();
        }

        public void Boost()
        {
            _healthManager.IncreaseMaxHealth();
        }
    }
}