using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scoring;

public class EnemyBallContr : MonoBehaviour
{
    Rigidbody2D rb;
    EnemyBallColl coll;
    EnemyBarContr bar;
    TrailRenderer trail;

    public float ballSp;
    public float ballMinHeight;
    public Vector2 lastPos;

    RaycastHit2D rayBall;
    float rayDis;
    public Vector2 launchStart;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<EnemyBallColl>();
        bar = FindObjectOfType<EnemyBarContr>();
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();

        lastPos = rb.position;
        //rb.velocity = new Vector2(Random.Range(0.2f, 1) * Mathf.Sign(rb.position.x), -1);
        rb.velocity = launchStart;
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
        rayDis = Mathf.Clamp(ballSp * Time.fixedDeltaTime, .25f, 100);
        Vector2 nextPos = rb.velocity.normalized * rayDis;
        rayBall = Physics2D.Raycast(rb.position, nextPos, rayDis);
        Debug.DrawRay(rb.position, nextPos, Color.red);

        if (rayBall) coll.CollHandle(rayBall, rayDis); else { coll.relay = false; }
    }

    public void DetLastRay()
    {
        Vector2 nextPos = rb.velocity.normalized * rayDis;
        rayBall = Physics2D.Raycast(rb.position, nextPos, rayDis);
        Debug.DrawRay(rb.position, nextPos, Color.green);

        if (rayBall) coll.CollHandle(rayBall, rayDis); else { coll.relay = false; }
    }

    void DetHeight()
    {
        if (rb.position.y < ballMinHeight)
        {
            StartCoroutine(WaitRespawn());
        }
    }

    IEnumerator WaitRespawn()
    {
        rb.position = new Vector2(0, 100);
        transform.position = rb.position;
        lastPos = rb.position;
        rb.velocity = Vector2.zero;
        trail.Clear();

        yield return new WaitUntil(() => Mathf.Round(bar.transform.position.x) == 0);

        rb.position = new Vector2(0, -3);
        transform.position = rb.position;
        lastPos = rb.position;
        rb.velocity = new Vector2(Random.Range(0.2f, 1) * Mathf.Sign(rb.position.x), -1);
        trail.Clear();
    }
}
