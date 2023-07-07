using System.Collections;
using System.Collections.Generic;
using Brick;
using UnityEngine;

public class EnemyBallColl : MonoBehaviour
{
    Rigidbody2D rb;
    EnemyBallContr ball;
    EnemyBarContr bar;
    TrailRenderer trail;

    [HideInInspector]
    public bool relay;

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
            case "ReverseBrick":
                TypeReverse(ray, dis);
                break;
            case "DefaultBounce":
                TypeDefault(ray, dis);
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
        rb.position = ray.point + rb.velocity.normalized * (dis - ray.distance);
        Debug.DrawRay(ray.point, rb.position - ray.point, Color.blue);

        trail.SetPosition(trail.positionCount-1, ray.point);

        bar.CollBarHit();

        if (relay) return;

        relay = true;
        ball.DetRay();
    }
    
    private void TypeReverse(RaycastHit2D ray, float dis)
    {
        rb.velocity = -rb.velocity;
        rb.position = ray.point + rb.velocity.normalized * (dis - ray.distance);
        Debug.DrawRay(ray.point, rb.position - ray.point, Color.blue);

        trail.SetPosition(trail.positionCount-1, ray.point);

        if (relay) return;

        relay = true;
        ball.DetRay();
    }


    public void TypeDefault(RaycastHit2D ray, float dis)
    {
        rb.velocity = Vector2.Reflect(rb.velocity, ray.normal);
        rb.position = ray.point + rb.velocity.normalized * (dis - ray.distance);
        Debug.DrawRay(ray.point, rb.position - ray.point, Color.blue);

        trail.SetPosition(trail.positionCount-1, ray.point);
        
        if (relay) return;

        relay = true;
        ball.DetRay();
    }

    #endregion
}
