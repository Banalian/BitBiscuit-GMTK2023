using Audio;
using UnityEngine;

namespace UI
{
    public class SoundMusicUIHandler : MonoBehaviour
    {
        private bool _isSoundOn = true;
        
        private bool _isMusicOn = true;
        
        [SerializeField]
        private GameObject soundCross;
        
        [SerializeField]
        private GameObject musicCross;
        
        public void ChangeSound()
        {
            AudioManager.Instance.Play(SoundBank.MenuClick);
            _isSoundOn = !_isSoundOn;
            soundCross.SetActive(!_isSoundOn);
            AudioManager.Instance.SetSoundVolume(_isSoundOn ? 1f : 0f);
        }
        
        public void ChangeMusic()
        {
            AudioManager.Instance.Play(SoundBank.MenuClick);
            _isMusicOn = !_isMusicOn;
            musicCross.SetActive(!_isMusicOn);
            AudioManager.Instance.SetMusicVolume(_isMusicOn ? 1f : 0f);
        }
    }
}