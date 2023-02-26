using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject defeat;

    private bool isDead = false;

    private Animator ui;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        pM = GetComponent<Player_Movement>();
        pW = GetComponent<Player_Weapon>();
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        ui = GameObject.Find("Health").GetComponent<Animator>();
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

        ui.SetInteger("health", health);
        ui.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.transform.tag == "PlayerDamage" && !pM.isRolling && canHurt)
        {
            health--;
            Knockback();

            CameraShake.Instance.DoShake(.1f, .05f);

            canHurt = false;
            animator.SetBool("isHurt", !canHurt);
            animator.Play("Hurt", 1);
            Invoke("Hurt", hurtTime);

            if (health == 0)
            {
                Death();
            }

            Sound.Instance.Play(2);
        }
    }

    private void Knockback()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(-transform.localScale.x * knockbackForce, knockbackForce / 1.5f));
    }

    private void Hurt()
    {
        canHurt = true;
        animator.SetBool("isHurt", !canHurt);
    }

    private void Death()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);
        pM.enabled = false;
        pW.enabled = false;
        defeat.SetActive(true);

        Invoke("Reload", 4.5f);
    }

    private void Reload()
    {
        SceneManager.LoadScene("BossStage");
    }
}
