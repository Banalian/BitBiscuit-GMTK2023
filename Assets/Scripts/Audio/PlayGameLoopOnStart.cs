using System;
using UnityEngine;

namespace Audio
{
    public class PlayGameLoopOnStart : MonoBehaviour
    {
        [SerializeField]
        private string gameLoopName = "GameLoop";
        
        [SerializeField]
        private float easeInTime = 3f;
        
        private void Start()
        {
            AudioManager.Instance.Play(gameLoopName, easeInTime);
        }
    }
}