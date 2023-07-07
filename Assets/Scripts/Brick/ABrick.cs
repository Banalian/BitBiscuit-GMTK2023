using System;
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
        /// The GameObject is a reference to the brick, and the int represents the damage taken.
        /// This is fired before the health manager updates.
        /// </summary>
        public event UnityAction<GameObject, int> OnBrickDamaged;
        
        /// <summary>
        /// Event fired when the brick is destroyed.
        /// The GameObject is a reference to the brick, and the bool is true if it's a manual destroy by the player.
        /// </summary>
        public event UnityAction<GameObject, bool> OnBrickDestroyed;

        /// <summary>
        /// Score you gain when the brick is destroyed
        /// </summary>
        [field:SerializeField] 
        public int ScoreValue { get; protected set; } = 1;

        protected virtual void Awake()
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
            OnBrickDamaged?.Invoke(gameObject, damage);
            
            _healthManager.Damage(damage);
        }

        public virtual void Destroy(bool manual = false)
        {
            _healthManager.OnDeathEvent -= OnDeath;
            
            OnBrickDestroyed?.Invoke(gameObject, manual);
        }

        public int GetScoreValue()
        {
            return ScoreValue;
        }

        /// <summary>
        /// Callback used for when the brick gets damaged. In child classes, should usually not be extended.
        /// Instead, extend the Destroy method.
        /// </summary>
        /// <param name="brick">The brick that died (here, ourself)</param>
        protected virtual void OnDeath(GameObject brick)
        {
            Destroy();
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            // if it's the player (check tag), take a hit with the given amount of damage that the player has.
            if(other.gameObject.CompareTag("EnemyBall"))
            {
                Hit(other.gameObject, 1);
            }
        }
    }
}