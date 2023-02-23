using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public float movementResponsiveness;
    public float speed;
    private int horizontal;
    private Vector2 velocityChange;

    public LayerMask floorLayer;
    public float jumpHeight;
    public int nbJumpsMax;
    private int nbJumps;
    [HideInInspector]
    public bool isGrounded = false;
    private bool jumped = false;

    public float rollDistance;
    public float rollTime;
    public float rollCooldown;
    private float inviTimer;
    private float rollTimer = 0;
    private bool isRolling = false;

    private enum Looking
    {
        Right,
        Left
    }
    private Looking currentLook;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();

        nbJumps = nbJumpsMax;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRoll();

        if (!isRolling)
        {
            PlayerMovement();
            SpriteFlipping();

            PlayerJump();
        }

        GroundCheck();
        UpdateAnims();
    }

    private void FixedUpdate()
    {
        rb.AddForce(velocityChange * movementResponsiveness);
    }

    private void PlayerMovement()
    {
        horizontal = 0;

        if (Input.GetButton("Right"))
        {
            horizontal = 1;
        }
        
        if (Input.GetButton("Left"))
        {
            horizontal = -1;
        }

        if (Input.GetButton("Left") && Input.GetButton("Right"))
        {
            horizontal = 0;
        }

        float strafeVelocity = horizontal * speed;
        Vector2 targetVelocity = new Vector2(strafeVelocity, rb.velocity.y);

        Vector2 currentVelocity = new Vector2(rb.velocity.x, rb.velocity.y);

        velocityChange = targetVelocity - currentVelocity;
    }

    private void SpriteFlipping()
    {
        if (Input.GetButtonDown("Right") || horizontal == 1)
        {
            currentLook = Looking.Right;
            rb.transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetButtonDown("Left") || horizontal == -1)
        {
            currentLook = Looking.Left;
            rb.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && nbJumps > 0 && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            nbJumps--;
            jumped = true;
            isGrounded = false;
        }

        if (Input.GetButtonUp("Jump") && jumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (rb.velocity.y <= 0)
        {
            jumped = false;
        }
    }

    private void GroundCheck()
    {
        Collider2D ground = Physics2D.OverlapCapsule(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(0.05f, 1.5f), CapsuleDirection2D.Vertical, 0, floorLayer);

        if (ground != null && !jumped && rb.velocity.y > -0.1f && rb.velocity.y < 0.1f)
        {
            isGrounded = true;
            nbJumps = nbJumpsMax;
        }

        if (ground == null)
        {
            isGrounded = false;
            nbJumps = 0;
        }
    }

    private void PlayerRoll()
    {
        if (rollTimer > -1)
        {
            rollTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Roll") && !isRolling && rollTimer <= 0)
        {
            velocityChange = new Vector2(0, 0);
            rb.velocity = new Vector2(0, rb.velocity.y);

            switch (currentLook)
            {
                case Looking.Right:
                    rb.AddForce(new Vector2(rollDistance, 0));
                    break;
                case Looking.Left:
                    rb.AddForce(new Vector2(-rollDistance, 0));
                    break;
                default:
                    break;
            }

            isRolling = true;
            inviTimer = rollTime;
            rollTimer = rollCooldown;
        }

        if (isRolling)
        {
            if (inviTimer <= 0)
            {
                isRolling = false;
                rb.velocity = new Vector2(rb.velocity.x - 5, rb.velocity.y);
            } 

            inviTimer -= Time.deltaTime;
        }
    }

    private void UpdateAnims()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isRolling", isRolling);
    }
}
