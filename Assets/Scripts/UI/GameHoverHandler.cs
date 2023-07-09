using System;
using System.Collections;
using Audio;
using Brick;
using Grid;
using Scoring;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameHoverHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameOverCanvas;
        
        [SerializeField]
        private TextMeshProUGUI scoreText;
        
        private CoreBrick _player;

        private void Start()
        {
             _player = FindObjectOfType<CoreBrick>();
             if(_player == null)
             {
                 StartCoroutine(WaitForPlayer());
             }
             else
             {
                 _player.OnBrickDestroyed += OnGameOver;
             }
            
        }

        private IEnumerator WaitForPlayer()
        {
            do
            {
                yield return new WaitForSeconds(0.1f);
                _player = FindObjectOfType<CoreBrick>();
            } while (_player == null);
            _player.OnBrickDestroyed += OnGameOver;
        }

        private void OnGameOver(GameObject arg0, bool arg1)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
            Destroy(ElementManager.Instance.gameObject);
            AudioManager.Instance.Play(SoundBank.GameOver);
            scoreText.SetText("Final Score : " + ScoreManager.Instance.GetRemainingScore() + "p");
        }
        
        public void Restart()
        {
            FindObjectOfType<AnimTransHandler>().DoIntro();

            StartCoroutine(WaitTrans());
        }

        IEnumerator WaitTrans()
        {
            yield return new WaitForSecondsRealtime(1);

            AudioManager.Instance.Play(SoundBank.MenuClick);
            Time.timeScale = 1;
            ElementManager.Instance.ResetSelected();
            ScoreManager.Instance.ResetScore();
            TooltipScreenSpaceUI.Instance.HideTooltip();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void Quit()
        {
            AudioManager.Instance.Play(SoundBank.MenuClick);
            Application.Quit();
        }
    }
}