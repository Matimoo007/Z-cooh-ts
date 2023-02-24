using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int maxHealth;
    private int health;

    public float knockbackForce;

    private Player_Movement pM;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        pM = GetComponent<Player_Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "PlayerDamage" && !pM.isRolling)
        {
            health--;
            Knockback();
        }
    }

    private void Knockback()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(-transform.localScale.x * knockbackForce, knockbackForce / 2));
    }
}
