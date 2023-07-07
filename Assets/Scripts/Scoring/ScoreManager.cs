using UnityEngine;
using UnityEngine.Events;

namespace Scoring
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance{ get; private set; }

        [Tooltip("The current score value")]
        [field: SerializeField] 
        public int Score { get; private set; } = 0;

        // Event fired everytime the score changes, it contains the amount by which the score was changed.
        public UnityAction<int> OnScoreChangedEvent;

        public ScoreManager(UnityAction<int> onScoreChangedEvent)
        {
            OnScoreChangedEvent = onScoreChangedEvent;
        }

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
        /// Modifies the score value by the inputted parameter, can be given a negative value to decrease score
        /// </summary>
        /// <param name="scoreAmount"></param>
        public void AddScore(int scoreAmount)
        {
            this.Score += scoreAmount;
            OnScoreChangedEvent?.Invoke(scoreAmount);
        }
        
        /// <summary>
        /// Resets the score to zero.
        /// </summary>
        private void ResetScore()
        {
            this.Score = 0;
        }
    }
}
