using Audio;
using UnityEngine;

namespace UI
{
    public class CreditUIHandler : MonoBehaviour
    {
        private bool _isCreditOn = false;
        
        [SerializeField]
        private GameObject creditPanel;
        
        public void ChangeCredit()
        {
            AudioManager.Instance.Play(SoundBank.MenuClick);
            _isCreditOn = !_isCreditOn;
            creditPanel.SetActive(_isCreditOn);
        }
    }
}