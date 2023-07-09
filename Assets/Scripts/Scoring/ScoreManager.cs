using UnityEngine;
using UnityEngine.Events;

namespace Scoring
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance{ get; private set; }
        
        [Tooltip("Base amount to give when the game starts")]
        [field: SerializeField]
        public int BaseScore { get; private set; } = 0;

        [Tooltip("The Total score value")]
        [field: SerializeField] 
        public int TotalPoints { get; private set; } = 0;
        
        [Tooltip("The amount spent")]
        [field: SerializeField]
        public int SpentPoints { get; private set; } = 0;

        [Tooltip("Current score multiplier")]
        [field: SerializeField]
        public float ScoreMultiplier { get; private set; }= 1;

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
            ResetScore(false);
        }
        
        /// <summary>
        /// Add or remove a value to the score multiplier
        /// </summary>
        /// <param name="modifier"> value to add or retract</param>
        public void ModifyMultiplier (float modifier)
        {
            ScoreMultiplier += modifier;
            // round it to 2 decimal
            ScoreMultiplier = Mathf.Round(ScoreMultiplier * 100f) / 100f;
        }
        
        /// <summary>
        /// Modifies the score value by the inputted parameter, can be given a negative value to decrease score
        /// </summary>
        /// <param name="scoreAmount"></param>
        public void AddScore(int scoreAmount)
        {
            this.TotalPoints += (int) Mathf.Round(scoreAmount * ScoreMultiplier);
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
        /// <param name="resetEvent">If true, the OnScoreChangedEvent will be reset, otherwise it will be invoked with a value of 0</param>
        public void ResetScore(bool resetEvent = true)
        {
            this.TotalPoints = BaseScore;
            this.SpentPoints = 0;
            this.ScoreMultiplier = 1;
            
            if (resetEvent)
            {
                OnScoreChangedEvent = null;
            }
            else
            {
                OnScoreChangedEvent?.Invoke(0);
            }
        }
    }
}
