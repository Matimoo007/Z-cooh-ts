using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject player;
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(player.transform.position.x * 0.15f, (player.transform.position.y + 2.9f) * 0.15f, -10f), followSpeed * Time.deltaTime);
    }
}
