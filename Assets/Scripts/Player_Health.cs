using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int maxHealth;
    private int health;

    public float knockbackForce;
    public float hurtTime;
    private bool canHurt = true;

    private Player_Movement pM;
    private Player_Weapon pW;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        pM = GetComponent<Player_Movement>();
        pW = GetComponent<Player_Weapon>();
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log("Player : " + health);

        if (health < 0)
        {
            health = 0;
        }

        if (isDead)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "PlayerDamage" && !pM.isRolling && canHurt)
        {
            health--;
            Knockback();

            canHurt = false;
            animator.Play("Hurt", 0);
            StartCoroutine("Flicker");
            Invoke("Hurt", hurtTime);

            if (health == 0)
            {
                Death();
            }
        }
    }

    private void Knockback()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(-transform.localScale.x * knockbackForce, knockbackForce / 1.5f));
    }

    private IEnumerator Flicker()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            Debug.Log("test");
            sprite.color = Color.black;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Hurt()
    {
        canHurt = true;
        StopCoroutine("Flicker");
    }

    private void Death()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);
        pM.enabled = false;
        pW.enabled = false;
    }
}
