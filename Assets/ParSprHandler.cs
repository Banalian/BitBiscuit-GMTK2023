using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParSprHandler : MonoBehaviour
{
    ParticleSystem par;
    Rigidbody2D rbBall;

    void Awake()
    {
        par = GetComponent<ParticleSystem>();
        rbBall = FindObjectOfType<Rigidbody2D>();
    }

    /// <summary>
    /// Emits block-break particles. Feed it the block position, and, poof. Simple as.
    /// </summary>
    /// <param name="pos"></param>
    [ContextMenu("fart")]
    public void EmitParticles(Vector2 pos)
    {
        transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, Vector2.Reflect(rbBall.velocity, Vector2.up)));
        transform.position = pos;
        par.Play();
    }
}
