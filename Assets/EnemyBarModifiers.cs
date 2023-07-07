using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarModifiers : MonoBehaviour
{
    Transform spr;

    void Awake()
    {
        spr = transform.GetChild(0);
        ModSizeUp();
    }

    public float sizeDur = 3;
    public float sizeWidth = 25;

    [ContextMenu("ModifierSizeUp")]
    public void ModSizeUp()
    {
        StartCoroutine(TimerSizeUp());
    }

    IEnumerator TimerSizeUp()
    {
        transform.localScale = new(sizeWidth, 1);
        spr.localScale = new(15/sizeWidth, 1);
        float nextScale = 1;

        float timer = sizeDur;
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            spr.localScale = new(Mathf.Lerp(spr.localScale.x, 1, Time.deltaTime*15), 1);

            yield return null;
        }

        transform.localScale = new(15, 1);
        spr.localScale = new(sizeWidth/15, 1);

        while (spr.localScale.x > 1.01f)
        {
            spr.localScale = new(Mathf.Lerp(spr.localScale.x, 1, Time.deltaTime * 15), 1);

            yield return null;
        }
    }
}
