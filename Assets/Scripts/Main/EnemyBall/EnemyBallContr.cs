using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scoring;

public class EnemyBallContr : MonoBehaviour
{
    Rigidbody2D rb;
    EnemyBallColl coll;
    TrailRenderer trail;

    public float ballSp;
    public float ballMinHeight;
    Vector2 lastPos;

    RaycastHit2D rayBall;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<EnemyBallColl>();
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();

        rb.velocity = new Vector2(3, -1);
    }

    void FixedUpdate()
    {
        DetHeight();
        DetRay();

        rb.velocity = rb.velocity.normalized*ballSp;
        lastPos = rb.position;
    }

    //sets ray between last and current position, if it detects anything calls CollHandle().
    public void DetRay()
    {
        float dis = (rb.position - lastPos).magnitude;
        rayBall = Physics2D.Raycast(lastPos, rb.position - lastPos, dis);
        Debug.DrawRay(lastPos, rb.position - lastPos, Color.red);

        if (rayBall) coll.CollHandle(rayBall, dis); else { coll.relay = false; }
    }

    void DetHeight()
    {
        if (rb.position.y < ballMinHeight)
        {
            rb.position = new Vector2(0, -3);
            lastPos = rb.position;
            rb.velocity = new Vector2(Random.Range(0.2f, 1) * Mathf.Sign(rb.position.x), -1);
            trail.Clear();

            //ScoreManager.Instance.AddScore(50);
        }
    }
}
