using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VFX : MonoBehaviour
{
    private Animator animator;

    private string clipName
    {
        get { return _clipName; }
        set
        {
            if (_clipName != value)
            {
                _clipName = value;
                DoVFX();
            }
        }
    }

    private AnimatorClipInfo[] clipInfo;
    private string _clipName;

    public GameObject[] vfx;
    public string[] associatedState;

    public Dictionary<string, GameObject> dict = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();

        for (int i = 0; i < vfx.Length; i++)
        {
            dict.Add(associatedState[i], vfx[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        clipName = clipInfo[0].clip.name;
    }

    private void DoVFX()
    {
        GameObject vfx;
        switch (clipName)
        {
            case "RollLeft":
                vfx = dict.GetValueOrDefault(_clipName);
                vfx.SetActive(false);
                vfx.SetActive(true);
                break;
            case "RollRight":
                vfx = dict.GetValueOrDefault(_clipName);
                vfx.SetActive(false);
                vfx.SetActive(true);
                break;
            default:
                break;
        }
    }
}
