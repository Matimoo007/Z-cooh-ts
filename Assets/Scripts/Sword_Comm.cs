using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Comm : MonoBehaviour
{
    Player_Weapon pW;
    private bool recoil = false;

    private void Start()
    {
        pW = GameObject.Find("Player").GetComponent<Player_Weapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (recoil) return;

        if (collision.transform.tag == "PlayerDamage" || collision.transform.tag == "Boss")
        {
            recoil = true;
            pW.WeaponRecoil();
            Invoke("CanRecoil", 0.25f);
        }
    }

    private void CanRecoil()
    {
        recoil = false;
    }
}
