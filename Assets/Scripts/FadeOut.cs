using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        anim.Play("OhShit");
        Invoke("Off", 1.0f);
    }

    private void Off()
    {
        gameObject.SetActive(false);
    }
}
