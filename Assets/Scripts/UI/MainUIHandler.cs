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