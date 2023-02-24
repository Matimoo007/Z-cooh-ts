using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public GameObject swordObject;
    public GameObject swordSide, swordUp, swordDown;

    public float swordCooldown;
    private float swordTimer;

    public float recoilForce;
    public float pogoForce;

    private Animator swordAnimator;
    private Player_Movement pM;
    private Rigidbody2D rb;

    private enum SwordDirection
    {
        Side,
        Up,
        Down
    }

    private SwordDirection swordDir = SwordDirection.Side;
    private int vertical;

    // Start is called before the first frame update
    void Start()
    {
        swordAnimator = swordObject.GetComponent<Animator>();
        pM = GetComponent<Player_Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SwordPositionning();
        Attack();

        if (swordTimer > -1)
        {
            swordTimer -= Time.deltaTime;
        }
    }

    private void SwordPositionning()
    {
        if (Input.GetButton("Left") || Input.GetButton("Right"))
        {
            swordDir = SwordDirection.Side;
        }

        vertical = 0;

        if (Input.GetButton("Up"))
        {
            vertical = 1;
        }

        if (Input.GetButton("Down"))
        {
            vertical = -1;
        }

        if (Input.GetButton("Up") && Input.GetButton("Down"))
        {
            vertical = 0;
        }

        if (Input.GetButtonDown("Up") || vertical == 1)
        {
            swordDir = SwordDirection.Up;
        }

        if (Input.GetButtonDown("Down") || vertical == -1)
        {
            swordDir = SwordDirection.Down;
        }

        if (Input.GetButtonUp("Up") || Input.GetButtonUp("Down"))
        {
            swordDir = SwordDirection.Side;
        }
    }

    private void Attack()
    {
        if (pM.isRolling)
        {
            ResetSword();
            swordAnimator.SetBool("isIdle", true);
        }

        AnimatorClipInfo[] clipInfo = swordAnimator.GetCurrentAnimatorClipInfo(0);
        string clipName = clipInfo[0].clip.name;

        if (Input.GetButtonDown("Attack") && clipName == "SwordIdle" && swordTimer <= 0 && !pM.isRolling)
        {
            swordTimer = swordCooldown;

            switch (swordDir)
            {
                case SwordDirection.Side:
                    swordObject.transform.position = swordSide.transform.position;
                    swordAnimator.SetBool("isIdle", false);
                    swordAnimator.SetBool("isSide", true);
                    Invoke("ResetSword", 0.1f);
                    break;
                case SwordDirection.Up:
                    swordObject.transform.position = swordUp.transform.position;
                    swordAnimator.SetBool("isIdle", false);
                    swordAnimator.SetBool("isUp", true);
                    Invoke("ResetSword", 0.1f);
                    break;
                case SwordDirection.Down:
                    if (pM.isGrounded) return;
                    swordObject.transform.position = swordDown.transform.position;
                    swordAnimator.SetBool("isIdle", false);
                    swordAnimator.SetBool("isDown", true);
                    Invoke("ResetSword", 0.1f);
                    break;
                default:
                    break;
            }
        }
    }

    private void ResetSword()
    {
        swordAnimator.SetBool("isSide", false);
        swordAnimator.SetBool("isUp", false);
        swordAnimator.SetBool("isDown", false);
    }

    public void WeaponRecoil()
    {
        switch (swordDir)
        {
            case SwordDirection.Side:
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(-transform.localScale.x * recoilForce, 0));
                break;
            case SwordDirection.Up:
                //nothin
                break;
            case SwordDirection.Down:
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, pogoForce));
                break;
            default:
                break;
        }
    }
}
