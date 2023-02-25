using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bite : Boss_Attack
{
    GameObject player;
    Animator animator;

    private void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    protected override IEnumerator Attack()
    {
        transform.position = player.transform.position;
        float timer = 1.5f;
        while (timer > .5f)
        {
            timer -= Time.deltaTime;

            transform.position = Vector2.Lerp(transform.position, player.transform.position, 50 * Time.deltaTime);
            yield return null;
        }

        animator.Play("Bite");

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            transform.position = Vector2.Lerp(transform.position, player.transform.position, 50 * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(.8f);

        AttackOver();
    }
}
