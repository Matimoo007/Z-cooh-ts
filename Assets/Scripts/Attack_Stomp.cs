using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Stomp : Boss_Attack
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
        yield return null;
    }
}
