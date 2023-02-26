using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Homing : Boss_Attack
{
    GameObject player;
    Animator animator;
    Collider2D cd;

    GameObject spawn;

    Transform vfx;
    SpriteRenderer sprite;

    private void Awake()
    {
        player = GameObject.Find("Player");

        animator = GetComponent<Animator>();
        cd = GetComponent<Collider2D>();
        vfx = transform.Find("HomingVFX");
        sprite = GetComponent<SpriteRenderer>();

        spawn = GameObject.Find("TopEye_1");

        cd.enabled = false;
        vfx.gameObject.SetActive(false);
        sprite.enabled = false;
    }

    protected override IEnumerator Attack()
    {
        float timed;

        transform.position = spawn.transform.position;
        timed = 1f;
        while (timed >= 0)
        {
            timed -= Time.deltaTime;
            yield return null;
        }
        cd.enabled = true;
        vfx.gameObject.SetActive(true);
        sprite.enabled = true;

        Sound.Instance.Play(7);
        Vector2 target = new Vector2(0.4f, 5f);
        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime);
            yield return null;
        }

        var timer = 5f;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 5.3f);
            yield return null;
        }

        End();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !player.GetComponent<Player_Movement>().isRolling)
        {
            End();
        }
    }

    private void End()
    {
        animator.Play("Homing", 0);
        Invoke("AttackOver", .2f);
    }
}
