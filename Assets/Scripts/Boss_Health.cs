using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Health : MonoBehaviour
{
    private int maxHealth = 50;
    private int health;

    private Boss_Logic bL;
    private SpriteRenderer sprite;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        bL = GetComponent<Boss_Logic>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log("Boss : " + health);

        if (health < 0)
        {
            health = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "BossDamage")
        {
            health--;

            if(health == 15)
            {
                bL.NextStage();
            }
            else if(health == 35)
            {
                bL.NextStage();
            }

            if (health == 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        isDead = true;
    }
}
