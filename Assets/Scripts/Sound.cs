using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound Instance;

    public AudioSource[] sounds;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    public void Play(int i)
    {
        sounds[i].Play();
    }

    public void Stop(int i)
    {
        sounds[i].Stop();
    }
}
