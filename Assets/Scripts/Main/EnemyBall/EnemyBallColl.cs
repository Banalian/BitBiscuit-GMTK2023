using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brick;
using Scoring;

public class EnemyBallColl : MonoBehaviour
{
    Rigidbody2D rb;
    EnemyBallContr ball;
    EnemyBarContr bar;
    TrailRenderer trail;

    [HideInInspector]
    public bool relay;

    public GameObject prefabParBall;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = FindObjectOfType<EnemyBallContr>();
        bar = FindObjectOfType<EnemyBarContr>();
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();
    }

    //anytime it hits something, this gets called to choose which function to use as the interaction.
    //function is chosen by tag, add a tag and a case for new interactions.
    public void CollHandle(RaycastHit2D ray, float dis)
    {
        switch (ray.collider.gameObject.tag)
        {
            case "EnemyBar":
                TypeBar(ray, dis);
                break;
            case "DefaultBounce":
                TypeDefault(ray, dis);
                break;
            case "ReverseBrick":
                TypeReverse(ray, dis);
                break;
            case "CoreBrick":
                TypeCore(ray, dis);
                break;
        }
        
        if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Bricks"))
        {
            if (ray.collider.TryGetComponent(out IBrick brick))
            {
                brick.Hit(gameObject, 1);
            }
        }
    }

    //every function in this region is just the response to different objects.
    //types are fairly self explanatory from name alone.
    #region HitTypes

    public void TypeBar(RaycastHit2D ray, float dis)
    {
        rb.velocity = Vector2.Reflect(rb.velocity, ray.normal);
        rb.position = ray.point + ray.normal * .25f;
        ball.lastPos = ray.point + ray.normal * .01f;
        Debug.DrawRay(ray.point, rb.position - ray.point, Color.blue);

        bar.CollBarHit();

        if (relay) { ball.DetBarLose(); return; }

        relay = true;
        trail.SetPosition(trail.positionCount - 1, rb.position);
        ball.DetLastRay();
    }

    public void TypeDefault(RaycastHit2D ray, float dis)
    {
        rb.velocity = Vector2.Reflect(rb.velocity, ray.normal);
        rb.position = ray.point + ray.normal * .25f;
        ball.lastPos = ray.point + ray.normal * .01f;
        Debug.DrawRay(ray.point, rb.position - ray.point, Color.blue);

        if (relay) { ball.DetBarLose(); return; }

        relay = true;
        trail.SetPosition(trail.positionCount - 1, rb.position);
        ball.DetLastRay();
    }

    private void TypeReverse(RaycastHit2D ray, float dis)
    {
        rb.velocity = -rb.velocity;
        rb.position = ray.point + ray.normal * .25f;
        ball.lastPos = ray.point + ray.normal * .01f;
        Debug.DrawRay(ray.point, rb.position - ray.point, Color.blue);

        if (relay) { ball.DetBarLose(); return; }

        relay = true;
        trail.SetPosition(trail.positionCount - 1, rb.position);
        ball.DetLastRay();
    }
    
    public void TypeCore(RaycastHit2D ray, float dis)
    {
        ParBallHandler prefab = Instantiate(prefabParBall, transform).GetComponent<ParBallHandler>();
        prefab.transform.parent = null;

        trail.transform.parent = prefab.transform;
        trail.transform.localPosition = Vector2.zero;

        prefab.StartVel(rb.velocity, ball);

        ball.StartCoroutine(ball.WaitRespawn());

        ScoreManager.Instance?.AddScore(10);
    }

    #endregion
}
