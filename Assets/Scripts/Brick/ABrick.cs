﻿using System;
using Audio;
using Grid;
using Scoring;
using UnityEngine;
using UnityEngine.Events;

namespace Brick
{
    /// <summary>
    /// Abstract brick containing the boilerplate code
    /// </summary>
    public abstract class ABrick : MonoBehaviour, IBrick
    {

        protected HealthManager _healthManager;
        
        /// <summary>
        /// Event fired when the brick takes damage
        /// The GameObject is a reference to the brick, and the int represents the damage taken and the new health
        /// This is fired before the health manager updates.
        /// </summary>
        public event UnityAction<GameObject, int, int> OnBrickDamaged;
        
        /// <summary>
        /// Event fired when the brick is destroyed.
        /// The GameObject is a reference to the brick, and the bool is true if it's a manual destroy by the player.
        /// </summary>
        public event UnityAction<GameObject, bool> OnBrickDestroyed;

        /// <summary>
        /// Event fired when the brick successfully gets a level up.
        /// The GameObject is a reference to the brick, and the int represents the new level
        /// </summary>
        public event UnityAction<GameObject, int> OnBrickLevelChanged;

        /// <summary>
        /// Points needed to buy the brick
        /// </summary>
        [field:SerializeField] 
        public int ScoreCost { get; protected set; } = 1;
        
        /// <summary>
        /// Score you gain when the brick is destroyed
        /// </summary>
        [field:SerializeField] 
        public int ScoreDestroyValue { get; protected set; } = 1;
        
        /// <summary>
        /// Score you gain when the brick is hit
        /// </summary>
        [field:SerializeField] 
        public int ScoreHitValue { get; protected set; } = 1;

        /// <summary>
        /// Current level of the brick
        /// </summary>
        [field:SerializeField] 
        public int CurrentLevel { get; protected set; } = 1;
        
        /// <summary>
        /// Maximum allowed level
        /// </summary>
        [field:SerializeField] 
        public int MaximumLevel { get; protected set; } = 1;
        
        /// <summary>
        /// Base upgrade cost
        /// </summary>
        [SerializeField]
        protected int BaseUpgradeCost = 5;
        
        /// <summary>
        /// A potentially multi line description of the brick
        /// </summary>
        [SerializeField]
        [TextArea]
        protected string description = "Default description";
        

        protected virtual void Start()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            _healthManager = GetComponent<HealthManager>();
            if (_healthManager != null)
            {
                _healthManager.Reset();
            }

            _healthManager.OnDeathEvent += OnDeath;
        }
        
        public virtual void Hit(GameObject ball, int damage)
        {
            OnBrickDamaged?.Invoke(gameObject, damage, _healthManager.Health - damage);
            
            _healthManager.Damage(damage);
            
            ScoreManager.Instance?.AddScore(
                damage >= _healthManager.Health? 
                    this.ScoreDestroyValue :
                    this.ScoreHitValue
                );
            
            AudioManager.Instance.Play(SoundBank.BrickDamage);
        }

        public virtual void DestroyBrick(bool manual = false)
        {
            _healthManager.OnDeathEvent -= OnDeath;
            
            OnBrickDestroyed?.Invoke(gameObject, manual);

            AudioManager.Instance.Play(SoundBank.BrickBreak);

            ElementManager.Instance.DeleteElement(this.gameObject);
        }

        public int GetScoreValue()
        {
            return ScoreDestroyValue;
        }

        /// <summary>
        /// Callback used for when the brick gets damaged. In child classes, should usually not be extended.
        /// Instead, extend the Destroy method.
        /// </summary>
        /// <param name="brick">The brick that died (here, ourself)</param>
        protected virtual void OnDeath(GameObject brick)
        {
            DestroyBrick();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            // if it's the player (check tag), take a hit with the given amount of damage that the player has.
            if(other.gameObject.CompareTag("EnemyBall"))
            {
                Hit(other.gameObject, 1);
            }
        }

        /// <summary>
        /// Add a given value to the score destroy value
        /// </summary>
        /// <param name="currencyUpgrade">Amount to add to the destroy score</param>
        public void UpgradeDeathScore(int currencyUpgrade)
        {
            ScoreDestroyValue += currencyUpgrade;
        }


        /// <summary>
        /// Check if a brick can be leveled up
        /// </summary>
        /// <returns>true if you can level it up, false otherwise</returns>
        public bool CanLevelUp()
        {
            return CurrentLevel < MaximumLevel;
        }

        /// <summary>
        /// Try to level up the brick
        /// </summary>
        /// <returns>true if it succeeded, false otherwise</returns>
        public bool LevelUp()
        {
            if (!CanLevelUp())
            {
                return false;
            }

            CurrentLevel++;
            
            var boostables = gameObject.GetComponentsInChildren<IBoostableBrick>();
            foreach (var boostable in boostables)
            {
                boostable.Boost();
            }
            
            AudioManager.Instance.Play(SoundBank.Upgrade);

            OnBrickLevelChanged?.Invoke(gameObject, CurrentLevel);

            return true;
        }

        /// <summary>
        /// Function to get the upgrade cost of the brick. Please override this function if you want to change the cost.
        /// </summary>
        /// <returns>the cost in point to upgrade the brick</returns>
        public virtual int GetUpgradeCost()
        {
            return (int)Math.Round((BaseUpgradeCost + CurrentLevel) * 2.3f);
        }

        public virtual string GetDescription()
        {
            return description;
        }
    }
}