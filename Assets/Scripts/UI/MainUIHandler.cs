using System.Collections;
using Audio;
using UnityEngine;

namespace UI
{
    public class MainUIHandler : MonoBehaviour
    {
        
        [SerializeField]
        private string playSceneName = "Game";
        
        public void Play()
        {
            AudioManager.Instance.Play(SoundBank.MenuClick);
            FindObjectOfType<AnimTransHandler>().DoIntro();

            StartCoroutine(WaitPlay());
        }

        IEnumerator WaitPlay()
        {
            yield return new WaitForSecondsRealtime(1);

            // launch the game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(playSceneName);
        }

        public void Quit()
        {
            AudioManager.Instance.Play(SoundBank.MenuClick);
            
            // quit the game
            Application.Quit();
        }
    }
}