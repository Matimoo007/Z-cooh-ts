using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Tentacles : Boss_Attack
{
    private static int rand = 0;

    private void Awake()
    {
        rand++;
        if (rand > 3)
        {
            rand = 0;
        }
    }

    protected override IEnumerator Attack()
    {
        Vector2 t1;
        Vector2 t2;
        if (rand == 0)
        {
            //bL
            transform.localScale = new Vector2(3, 1);
            t1 = new Vector2(-13.5f, -2.5f);
            t2 = new Vector2(-3f, -2.5f);
            transform.position = new Vector2(-18f, -2.5f);
        }
        else if (rand == 1)
        {
            //tR
            transform.localScale = new Vector2(-3, 1);
            t1 = new Vector2(13.5f, -.5f);
            t2 = new Vector2(3f, -.5f);
            transform.position = new Vector2(18f, -.5f);
        }
        else if (rand == 2)
        {
            //tL
            transform.localScale = new Vector2(3, 1);
            t1 = new Vector2(-13.5f, -.5f);
            t2 = new Vector2(-3f, -.5f);
            transform.position = new Vector2(-18f, -.5f);
        }
        else
        {
            //bR
            transform.localScale = new Vector2(-3, 1);
            t1 = new Vector2(13.5f, -2.5f);
            t2 = new Vector2(3f, -2.5f);
            transform.position = new Vector2(18f, -2.5f);
        }
        Vector2 reset = transform.position;

        while (Vector3.Distance(transform.position, t1) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, t1, Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        while (Vector3.Distance(transform.position, t2) > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, t2, 5 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        while (Vector3.Distance(transform.position, reset) > 0.05f)
        {
            transform.position = Vector2.Lerp(transform.position, reset, 5 * Time.deltaTime);
            yield return null;
        }


        AttackOver();
    }
}
