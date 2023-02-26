using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bite : Boss_Attack
{
    GameObject player;
    Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    protected override IEnumerator Attack()
    {
        float timed;

        transform.position = player.transform.position;
        float timer = 1.5f;
        while (timer > .5f)
        {
            timer -= Time.deltaTime;

            transform.position = Vector2.Lerp(transform.position, player.transform.position, 50 * Time.deltaTime);
            yield return null;
        }

        animator.Play("Bite");
        Sound.Instance.Play(4);

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            transform.position = Vector2.Lerp(transform.position, player.transform.position, 50 * Time.deltaTime);
            yield return null;
        }
        timed = .8f;
        while (timed >= 0)
        {
            timed -= Time.deltaTime;
            yield return null;
        }

        AttackOver();
    }
}
