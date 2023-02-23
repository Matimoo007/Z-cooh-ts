using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public GameObject swordIdle, swordLeft, swordRight, swordUp, swordDown;

    private enum SwordDirection
    {
        Idle,
        Right,
        Left,
        Up,
        Down
    }

    private SwordDirection swordDir;
    private SwordDirection lastDir;

    private int vertical;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SwordPositionning();

        if (Input.GetButtonDown("Attack"))
        {

        }
    }

    private void SwordPositionning()
    {
        if (Input.GetButtonDown("Left"))
        {
            swordDir = SwordDirection.Left;
        }

        if (Input.GetButtonDown("Right"))
        {
            swordDir = SwordDirection.Right;
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
}
