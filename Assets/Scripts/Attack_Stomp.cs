using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Stomp : Boss_Attack
{
    private Transform one, two, three;

    private void Awake()
    {
        one = transform.Find("1");
        two = transform.Find("2");
        three = transform.Find("3");
        one.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        three.gameObject.SetActive(false);
    }

    protected override IEnumerator Attack()
    {
        one.localPosition = new Vector2(1, one.localPosition.y);
        two.localPosition = new Vector2(2, two.localPosition.y);
        three.localPosition = new Vector2(3, three.localPosition.y);

        int rand = Random.Range(0, 2);

        Vector2 target;
        if (rand == 0)
        {
            transform.localScale = new Vector2(1, 1);
            transform.position = new Vector2(11, -2.75f);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
            transform.position = new Vector2(-11, -2.75f);
        }

        yield return new WaitForSeconds(.9f);

        three.gameObject.SetActive(true);
        yield return new WaitForSeconds(.15f);
        two.gameObject.SetActive(true);
        yield return new WaitForSeconds(.15f);
        one.gameObject.SetActive(true);

        while (three.localPosition.x > -24)
        {
            yield return new WaitForSeconds(.1f);

            three.localPosition = new Vector2(three.localPosition.x - 3, three.localPosition.y);
            yield return new WaitForSeconds(.15f);
            two.localPosition = new Vector2(two.localPosition.x - 3, two.localPosition.y);
            yield return new WaitForSeconds(.15f);
            one.localPosition = new Vector2(one.localPosition.x - 3, one.localPosition.y);
        }

        one.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        three.gameObject.SetActive(false);
        AttackOver();
    }
}
