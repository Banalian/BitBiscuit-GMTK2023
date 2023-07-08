using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarModifiers : MonoBehaviour
{
    SpriteRenderer spr;
    BoxCollider2D coll;

    void Awake()
    {
        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    public float sizeDur = 3;
    public float sizeWidth = 6;

    [ContextMenu("ModifierSizeUp")]
    public void ModSizeUp()
    {
        StartCoroutine(TimerSizeUp());
    }

    IEnumerator TimerSizeUp()
    {
        coll.size = new(sizeWidth/4*0.75f, 1);

        float sprSize = sizeWidth / 4 * 1.5f;
        float timer = sizeDur;
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            spr.size = new(Mathf.Lerp(spr.size.x, sprSize, Time.deltaTime*15), 1);

            yield return null;
        }

        coll.size = new(0.75f, 1);

        while (spr.size.x > 1.51f)
        {
            spr.size = new(Mathf.Lerp(spr.size.x, 1.5f, Time.deltaTime*15), 1);

            yield return null;
        }

        spr.size = new(1.5f, 1);
    }
}
