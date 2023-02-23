using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public GameObject swordObject;
    public GameObject swordSide, swordUp, swordDown;

    private Animator swordAnimator;
    private Player_Movement pM;

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
    }

    // Update is called once per frame
    void Update()
    {
        SwordPositionning();
        Attack();
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
    }

    private void Attack()
    {
        AnimatorClipInfo[] clipInfo = swordAnimator.GetCurrentAnimatorClipInfo(0);
        string clipName = clipInfo[0].clip.name;

        if (Input.GetButtonDown("Attack") && clipName == "SwordIdle")
        {
            switch (swordDir)
            {
                case SwordDirection.Side:
                    swordObject.transform.position = swordSide.transform.position;
                    swordAnimator.SetBool("isSide", true);
                    Invoke("ResetSword", 0.1f);
                    break;
                case SwordDirection.Up:
                    swordObject.transform.position = swordUp.transform.position;
                    swordAnimator.SetBool("isUp", true);
                    Invoke("ResetSword", 0.1f);
                    break;
                case SwordDirection.Down:
                    if (pM.isGrounded) return;
                    swordObject.transform.position = swordDown.transform.position;
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
}
