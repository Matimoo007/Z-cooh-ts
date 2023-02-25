using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Homing : Boss_Attack
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    protected override IEnumerator Attack()
    {
        Vector2 target = new Vector2(-2.6f, 3.5f);
        while (true)
        {

        }

        while (true)
        {

        }

        AttackOver();
    }
}
