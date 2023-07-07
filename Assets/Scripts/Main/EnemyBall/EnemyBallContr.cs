using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallContr : MonoBehaviour
{
    Rigidbody2D rb;
    EnemyBallColl coll;

    public float ballSp;
    Vector2 lastPos;

    RaycastHit2D rayBall;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<EnemyBallColl>();

        rb.velocity = new Vector2(3, -1);
    }

    void FixedUpdate()
    {
        DetRay();

        rb.velocity = rb.velocity.normalized*ballSp;
        lastPos = rb.position;
    }

    //sets ray between last and current position, if it detects anything calls CollHandle().
    void DetRay()
    {
        float dis = (rb.position - lastPos).magnitude;
        rayBall = Physics2D.Raycast(lastPos, rb.position - lastPos, dis);
        Debug.DrawRay(lastPos, rb.position - lastPos, Color.red);

        if (rayBall) coll.CollHandle(rayBall, dis);
    }
}
