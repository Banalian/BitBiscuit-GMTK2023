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

    void Awake()
    {
        ball = FindObjectOfType<EnemyBallContr>();
    }

    void Update()
    {
        float posX = Mathf.MoveTowards(transform.position.x, ball.transform.position.x, Time.deltaTime * sp);
        transform.position = new Vector2(posX, height);
    }

    public void CollBarHit()
    {
        TotalHits++;

        // feel free to move this elsewhere to make it cleaner if you want to
        AudioManager.Instance?.Play(SoundBank.PaddleHit);
    }
}
