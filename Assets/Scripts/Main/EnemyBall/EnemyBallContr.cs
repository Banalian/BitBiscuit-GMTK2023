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
    [HideInInspector]
    public Vector2 lastPos;

    RaycastHit2D rayBall;
    RaycastHit2D rayPer;
    float rayDis;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<EnemyBallColl>();
        bar = FindObjectOfType<EnemyBarContr>();
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();

        lastPos = rb.position;
        rb.velocity = new Vector2(Random.Range(0.2f, 1) * Mathf.Sign(rb.position.x), -1);
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
        Vector2 nextPos = rb.velocity.normalized;

        rayBall = Physics2D.Raycast(rb.position, nextPos, rayDis);
        Debug.DrawRay(rb.position, nextPos * rayDis, Color.red);

        Vector2 perPos = Mathf.Sign(rb.velocity.x) * Mathf.Sign(rb.velocity.y) * -Vector2.Perpendicular(nextPos);
        rayPer = Physics2D.Raycast(rb.position, perPos, .25f);
        Debug.DrawRay(rb.position, perPos*.25f, Color.black);

        if (rayBall) coll.CollHandle(rayBall, rayDis); else 
        {
            if (rayPer)
            {
                coll.CollHandle(rayPer, 0.25f);
            }

            coll.relay = false; 
        }
    }

    public void DetLastRay()
    {
        Vector2 nextPos = rb.velocity.normalized;

        rayBall = Physics2D.Raycast(rb.position, nextPos, rayDis);
        Debug.DrawRay(rb.position, nextPos * rayDis, Color.green);

        if (rayBall) coll.CollHandle(rayBall, rayDis); else { coll.relay = false; DetBarLose(); }
    }

    [ContextMenu("TestBarLose")]
    public void DetBarLose()
    {
        if (rb.velocity.y >= 0) return;

        LayerMask mask = ~LayerMask.GetMask("Enemy");
        Vector2 velNorm = rb.velocity.normalized;

        float rayDis = (rb.position.y+3.75f)/-velNorm.y;
        RaycastHit2D rayFirst = Physics2D.Raycast(rb.position, velNorm, rayDis, mask);
        Debug.DrawRay(rb.position, velNorm * rayDis, Color.white);
        Debug.Log((bool)rayFirst);

        if (!rayFirst) { bar.LandPosCalc(rb.position+velNorm*rayDis, rayFirst.distance); return; }

        Vector2 raySecOff = rayFirst.point + rayFirst.normal * .25f;
        Vector2 raySecPer = Vector2.Reflect(velNorm, rayFirst.normal);
        float raySecDis = (raySecOff.y + 3.75f) / -velNorm.y;
        RaycastHit2D raySecond = Physics2D.Raycast(raySecOff, raySecPer, raySecDis, mask);
        Debug.DrawRay(raySecOff, raySecPer * raySecDis, Color.yellow);
        Debug.Log((bool)raySecond);

        if (!raySecond) { bar.LandPosCalc(raySecOff+raySecPer*raySecDis, rayFirst.distance+raySecond.distance); return; }

        Vector2 rayLastOff = raySecond.point + raySecond.normal * .25f;
        Vector2 rayLastPer = Vector2.Reflect(raySecPer, raySecond.normal);
        float rayLastDis = (rayLastOff.y + 3.75f) / -velNorm.y;
        RaycastHit2D rayLast = Physics2D.Raycast(rayLastOff, rayLastPer, rayLastDis, mask);
        Debug.DrawRay(rayLastOff, rayLastPer * rayLastDis, Color.red);
        Debug.Log((bool)rayLast);

        if (!rayLast) { bar.LandPosCalc(rayLastOff+rayLastPer*rayLastDis, rayFirst.distance+raySecond.distance+rayLast.distance); return; }
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

        bar.incoming = false;

        yield return new WaitUntil(() => Mathf.Round(bar.transform.position.x) == 0);

        rb.position = new Vector2(0, -3);
        transform.position = rb.position;
        lastPos = rb.position;
        rb.velocity = new Vector2(Random.Range(0.2f, 1) * Mathf.Sign(rb.position.x), -1);
        trail.Clear();
    }
}
