using UnityEngine;
using UnityEngine.Events;

namespace Scoring
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance{ get; private set; }

        [Tooltip("The Total score value")]
        [field: SerializeField] 
        public int TotalPoints { get; private set; } = 0;
        
        [Tooltip("The amount spent")]
        [field: SerializeField]
        public int SpentPoints { get; private set; } = 0;

        protected float scoreMultiplier = 1;

        // Event fired everytime the score changes, it contains the amount by which the score was changed.
        public UnityAction<int> OnScoreChangedEvent;

        private void Awake() 
        { 
            //If there is another scoreManager instance, and it's not me, delete myself.
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            //If not, i'm the only ScoreManager.
            else 
            { 
                Instance = this; 
            } 
        }
        
        /// <summary>
        /// Add or remove a value to the score multiplier
        /// </summary>
        /// <param name="modifier"> value to add or retract</param>
        public void ModifyMultiplier (float modifier)
        {
            scoreMultiplier += modifier;
            // round it to 1 decimal
            scoreMultiplier = Mathf.Round(scoreMultiplier * 10f) / 10f;
        }
        
        /// <summary>
        /// Modifies the score value by the inputted parameter, can be given a negative value to decrease score
        /// </summary>
        /// <param name="scoreAmount"></param>
        public void AddScore(int scoreAmount)
        {
            this.TotalPoints += scoreAmount;
            OnScoreChangedEvent?.Invoke(scoreAmount);
        }
        
        public bool TrySpendScore(int scoreAmount)
        {
            // check if we have enough points
            if (TotalPoints - SpentPoints - scoreAmount < 0)
            {
                return false;
            }
            SpentPoints += scoreAmount;
            OnScoreChangedEvent?.Invoke(-scoreAmount);
            return true;
        }
        
        public int GetRemainingScore()
        {
            return TotalPoints - SpentPoints;
        }
        
        /// <summary>
        /// Resets the score to zero.
        /// </summary>
        private void ResetScore()
        {
            this.TotalPoints = 0;
        }
    }
}
