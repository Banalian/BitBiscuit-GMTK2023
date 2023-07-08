using System;
using UnityEngine;

namespace Audio
{
    public class PlayGameLoopOnStart : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.Play("GameLoop");
        }
    }
}