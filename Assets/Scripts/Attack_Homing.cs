using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Homing : Boss_Attack
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
        Vector2 target = new Vector2(-2.6f, 2f);
        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime);
        }

        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, player.transform.position, 2 * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.gameObject.layer == 6)
        {
            animator.Play("Hit", 0);
            Invoke("AttackOver", .1f);
        }
    }
}
