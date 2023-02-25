using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleUI : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1);
    }
}
