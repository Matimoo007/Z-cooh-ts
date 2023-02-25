using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public GameObject swordMaster;
    public GameObject swordObject;
    public GameObject swordSide, swordDown;

    public float swordCooldown;
    private float swordTimer;

    public float recoilForce;
    public float pogoForce;

    private Animator swordAnimator;
    private Player_Movement pM;
    private Rigidbody2D rb;

    private enum SwordDirection
    {
        Left,
        Right,
        Down
    }

    private SwordDirection swordDir = SwordDirection.Right;

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
        swordMaster.transform.position = transform.position;

        SwordPositionning();
        Attack();

        if (swordTimer > -1)
        {
            swordTimer -= Time.deltaTime;
        }
    }

    private void SwordPositionning()
    {
        if (Input.GetButton("Down") && !pM.isGrounded)
        {
            swordDir = SwordDirection.Down;
        }
        else if (transform.localScale.x == 1)
        {
            swordDir = SwordDirection.Right;
        }
        else
        {
            swordDir = SwordDirection.Left;
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
                case SwordDirection.Right:
                    swordMaster.transform.localScale = new Vector2(1, 1);
                    swordObject.transform.position = swordSide.transform.position;
                    swordAnimator.SetBool("isIdle", false);
                    swordAnimator.SetBool("isSide", true);
                    Invoke("ResetSword", 0.1f);
                    break;
                case SwordDirection.Left:
                    swordMaster.transform.localScale = new Vector2(-1, 1);
                    swordObject.transform.position = swordSide.transform.position;
                    swordAnimator.SetBool("isIdle", false);
                    swordAnimator.SetBool("isSide", true);
                    Invoke("ResetSword", 0.1f);
                    break;
                case SwordDirection.Down:
                    swordObject.transform.position = swordDown.transform.position;
                    swordAnimator.SetBool("isIdle", false);
                    swordAnimator.SetBool("isDown", true);
                    Invoke("ResetSword", 0.1f);
                    break;
                default:
                    break;
            }

            Sound.Instance.Play(3);
        }
    }

    private void ResetSword()
    {
        swordAnimator.SetBool("isSide", false);
        swordAnimator.SetBool("isDown", false);
    }

    public void WeaponRecoil()
    {
        switch (swordDir)
        {
            case SwordDirection.Left:
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(-transform.localScale.x * recoilForce, 0));
                break;
            case SwordDirection.Right:
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(-transform.localScale.x * recoilForce, 0));
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
