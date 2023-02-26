using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Health : MonoBehaviour
{
    private int maxHealth = 75;
    private int health;

    private Animator animator;

    private Boss_Logic bL;

    [HideInInspector]
    public bool isDead = false;

    public GameObject victory;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        bL = GetComponent<Boss_Logic>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health < 0)
        {
            health = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.transform.tag == "BossDamage" && !bL.inIntro)
        {
            health--;
            animator.Play("Hurt", 5);

            if(health == 35)
            {
                bL.NextStage();
            }
            else if(health == 60)
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
        animator.Play("Death");
        victory.SetActive(true);
        isDead = true;
        bL.LogicStop();
        Invoke("Reload", 5f);
    }

    private void Reload()
    {
        SceneManager.LoadScene("Start");
    }
}
