using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWay : MonoBehaviour
{
    private Collider2D cd;
    private Player_Movement pM;
    private float onewayTimer;

    // Start is called before the first frame update
    void Start()
    {
        cd = GetComponent<Collider2D>();
        pM = GameObject.Find("Player").GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Down") && pM.isGrounded)
        {
            if (onewayTimer <= 0)
            {
                cd.enabled = false;
            }
            onewayTimer -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Down"))
        {
            onewayTimer = 0.2f;
            cd.enabled = true;
        }
    }
}
