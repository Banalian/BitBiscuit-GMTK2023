using System.Collections;
using Audio;
using UnityEngine;

namespace Brick
{
    /// <summary>
    /// Brick that modify the ball's trajectory
    /// </summary>
    public class BallTrajModifierBrick : ABrick
    {
        /// <summary>
        /// Range of the modifier (bigger = can influence from further away)
        /// </summary>
        [SerializeField] 
        protected float range = 1;

        /// <summary>
        /// Force to apply to the ball each fixed update when it's in range
        /// </summary>
        public float force;

        /// <summary>
        /// Duration of the modifier when activated. put a negative value to never Deactivate
        /// </summary>
        [SerializeField] 
        protected float duration = 1;

        /// <summary>
        /// Cooldown of the modifier
        /// </summary>
        [SerializeField] 
        protected float cooldown = 2;

        /// <summary>
        /// Sound to play when the brick is activated
        /// </summary>
        [SerializeField] 
        private SoundBank activationSound;
        
        /// <summary>
        /// If true, the modifier will only activate when the player is close to it (if the cooldown is over)
        /// </summary>
        [SerializeField]
        private bool ActivateWhenPlayerIsClose = false;

        public bool IsActivated { get; protected set; } = false;

        private Coroutine _activationCoroutine;

        public override void Initialize()
        {
            base.Initialize();
            if (!ActivateWhenPlayerIsClose)
            {
                _activationCoroutine = StartCoroutine(ActivationCoroutine());
            }
        }

        public override void DestroyBrick(bool manual = false)
        {
            if (_activationCoroutine != null)
            {
                StopCoroutine(_activationCoroutine);
            }
            Deactivate();
            base.DestroyBrick(manual);
        }


        private IEnumerator ActivationCoroutine()
        {
            Activate();
            if (duration < 0f)
            {
                // we're doing a while loop, as we need the coroutine to keep running to not
                // have a null coroutine.
                while (true)
                {
                    yield return new WaitForSeconds(1);
                }
            }
            yield return new WaitForSeconds(duration);
            Deactivate();
            yield return new WaitForSeconds(cooldown);
        }


        /// <summary>
        /// Check if a position is in range of the modifier
        /// </summary>
        /// <param name="transformPosition">position to check</param>
        /// <returns>true if it's in range, false otherwise</returns>
        public bool IsInRange(Vector3 transformPosition)
        {
            return Vector2.Distance(transform.position, transformPosition) <= range;
        }

        private void Activate()
        {
            IsActivated = true;
            AudioManager.Instance.Play(activationSound);
        }

        private void Deactivate()
        {
            IsActivated = false;
        }

        public void ReduceCooldown(float reduction = 1f)
        {
            cooldown -= reduction;
            if (cooldown <= 0f)
            {
                cooldown = 0.5f;
            }
        }
        
        
        public void TryActivate()
        {
            if (IsActivated)
            {
                return;
            }

            if (ActivateWhenPlayerIsClose)
            {
                _activationCoroutine = StartCoroutine(ActivationCoroutine());
            }
        }
    }
}