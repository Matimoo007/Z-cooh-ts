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
    }

    protected override IEnumerator Attack()
    {
        transform.position = player.transform.position;
        float timer = 1.5f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            transform.position = Vector2.Lerp(transform.position, player.transform.position, 5 * Time.deltaTime);
            yield return null;
        }
        animator.Play("Bite");
        float wait = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(wait);

        AttackOver();
    }
}
