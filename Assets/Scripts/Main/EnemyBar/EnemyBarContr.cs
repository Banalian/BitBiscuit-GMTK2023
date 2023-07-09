using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class EnemyBarContr : MonoBehaviour
{
    EnemyBallContr ball;
    public float sp;
    public float height;

    public int TotalHits;

    public bool incoming;
    float landPosX;
    float danger;

    public float boundaryX;

    void Awake()
    {
        ball = FindObjectOfType<EnemyBallContr>();
    }

    void Update()
    {
        if (incoming)
        {
            GoToLand();
        }
        else
        {
            Idle();
        }
    }

    void Idle()
    {
        float posX = Mathf.MoveTowards(transform.position.x, 0, Time.deltaTime * sp);
        transform.position = new Vector2(Mathf.Clamp(posX, -boundaryX, boundaryX), height);
    }

    void GoToLand()
    {
        float posX = Mathf.MoveTowards(transform.position.x, landPosX, Time.deltaTime * sp);
        transform.position = new Vector2(Mathf.Clamp(posX, -boundaryX, boundaryX), height);
    }

    public void LandPosCalc(Vector2 pos, float dis)
    {
        incoming = true;

        danger = dis/ball.ballSp;
        landPosX = pos.x;
    }

    public void CollBarHit()
    {
        TotalHits++;

        incoming = false;

        // feel free to move this elsewhere to make it cleaner if you want to
        AudioManager.Instance?.Play(SoundBank.PaddleHit);
    }
}
