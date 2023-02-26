using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject fade;

    private Animator anim;

    private void Start()
    {
        anim = fade.GetComponent<Animator>();

        fade.SetActive(false);
    }

    public void PlayGame()
    {
        fade.SetActive(true);
        Invoke("Next", 1.0f);    
    }

    private void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
