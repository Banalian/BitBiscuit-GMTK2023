using System.Collections;
using Audio;
using Brick;
using Grid;
using Scoring;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimTransHandler : MonoBehaviour
{
    public bool intro;

    void Awake()
    {
        if (intro)
        {
            GetComponent<Animator>().Play("Idle");
        }
    }

    public void DoIntro()
    {
        GetComponent<Animator>().Play("TransIntro");
    }
}
