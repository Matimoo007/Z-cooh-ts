using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Homing : Boss_Attack
{
    GameObject player;
    Animator animator;
    Collider2D cd;
    Transform vfx;

    GameObject spawn;

    private void Awake()
    {
        player = GameObject.Find("Player");

        animator = GetComponent<Animator>();
        cd = GetComponent<Collider2D>();
        vfx = transform.Find("HomingVFX");

        spawn = GameObject.Find("TopEye");

        cd.enabled = false;
        vfx.gameObject.SetActive(false);
    }

    protected override IEnumerator Attack()
    {
        transform.position = spawn.transform.position;
        yield return new WaitForSeconds(1f);
        cd.enabled = true;
        vfx.gameObject.SetActive(true);

        Vector2 target = new Vector2(0, 5f);
        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime);
            yield return null;
        }

        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, player.transform.position, 5 * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.gameObject.layer == 6)
        {
            animator.Play("Homing", 0);
            cd.enabled = false;
            Invoke("AttackOver", .2f);
        }
    }
}
