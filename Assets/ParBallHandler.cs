using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParBallHandler : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spr;
    ParticleSystem par;

    EnemyBallContr ball;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        par = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        spr.color += new Color(0, -Time.deltaTime, -Time.deltaTime); 
        if (spr.color.b < .42f)
        {
            Transform child = transform.GetChild(0);
            child.parent = ball.transform;
            child.localPosition = Vector2.zero;
            spr.color = Color.white;

            transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(rb.velocity, Vector2.up));
            par.Play();

            this.enabled = false;

            //Destroy(this.gameObject);
        }
    }

    public void StartVel(Vector2 vel, EnemyBallContr ballPre)
    {
        rb.velocity = vel;
        ball = ballPre;
        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
}
