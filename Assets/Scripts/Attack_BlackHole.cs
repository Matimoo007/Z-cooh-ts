using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_BlackHole : Boss_Attack
{
    Boss_Logic bL;
    GameObject player;
    Player_Movement pM;
    Rigidbody2D rb;
    Animator animator;

    SpriteRenderer ui;

    private void Awake()
    {
        bL = GameObject.Find("Coots").GetComponent<Boss_Logic>();
        player = GameObject.Find("Player");
        pM = player.GetComponent<Player_Movement>();
        rb = player.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ui = GameObject.Find("BHMash").GetComponent<SpriteRenderer>();
    }

    protected override IEnumerator Attack()
    {
        float timer;

        Sound.Instance.Play(10);
        bL.blackHole = true;
        timer = 1.5f;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        ui.enabled = true;
        pM.stopMovement = true; ;
        yield return new WaitForEndOfFrame();
        rb.velocity = Vector2.zero;
        animator.Play("BlackHoleStart");
        Sound.Instance.Play(6);

        int presses = 15;
        while (presses >= 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                presses -= 1;
            }
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, Time.deltaTime);
            yield return null;
        }

        animator.Play("BlackHoleEnd");
        End();
    }

    private void End()
    {
        ui.enabled = false;
        bL.blackHole = false;
        pM.stopMovement = false;
        Sound.Instance.Stop(6);
        Invoke("AttackOver", .67f);
    }
}
