using System;
using Audio;
using Grid;
using Scoring;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShopUIHandler : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI scoreText;
        
        
        private GameObject _lastBrick;
        
        [SerializeField]
        private GameObject defaultBrick;


        private void Start()
        {
            ScoreManager.Instance.OnScoreChangedEvent += OnScoreChanged;
        }

        private void OnScoreChanged(int amount)
        {
            scoreText.SetText(ScoreManager.Instance.Score + "p");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ElementManager.Instance.SetSelectedElement(defaultBrick);
                _lastBrick = null;
            }
        }

        /// <summary>
        /// Change the brick to set in the element manager
        /// </summary>
        /// <param name="brick">brick to set. If equal to the last one, goes back to a default one</param>
        public void ChangeBrick(GameObject brick)
        {
            AudioManager.Instance.Play(SoundBank.MenuClick);
            // if it's the same as the last one, we remove the selection
            if (_lastBrick == brick)
            {
                ElementManager.Instance.SetSelectedElement(defaultBrick);
                _lastBrick = null;
            }
            else
            {
                // if no toggle is on, we can't change the brick
                ElementManager.Instance.SetSelectedElement(brick);
                _lastBrick = brick;
            }
        }
    }
}