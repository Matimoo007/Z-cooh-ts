using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Health : MonoBehaviour
{
    public int maxHealth;
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
        Debug.Log(health);

        if (health < 0)
        {
            health = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "BossDamage")
        {
            health--;

            if(health == (maxHealth - maxHealth * 0.7f))
            {
                bL.NextStage();
            }
            else if(health == (maxHealth - maxHealth * 0.3f))
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
