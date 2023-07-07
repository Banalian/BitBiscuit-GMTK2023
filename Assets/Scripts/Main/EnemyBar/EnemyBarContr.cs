using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarContr : MonoBehaviour
{
    EnemyBallContr ball;
    public float sp;
    public float height;

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

    }
}
