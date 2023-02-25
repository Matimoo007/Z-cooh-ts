using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Logic : MonoBehaviour
{
    private string intro1 = "Intro1";
    public float atkTime1;
    public GameObject[] atk1;
    public string[] anim1;

    private string intro2 = "Intro2";
    public float atkTime2;
    public GameObject[] atk2;
    public string[] anim2;

    public float atkTime3;

    private Animator animator;
    private float atkTime;
    private float atkTimer;

    private enum BossStage
    {
        Stage1,
        Stage2,
        Stage3
    }
    private BossStage currentStage = BossStage.Stage1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(intro1, 0);
        atkTime = atkTime1;
        atkTimer = atkTime;

        StartCoroutine("LogicUpdate");
    }

    private IEnumerator LogicUpdate()
    {
        float wait = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(wait);

        while (true)
        {
            atkTimer -= Time.deltaTime;
            if (atkTimer <= 0)
            {
                atkTimer = atkTime + Random.Range(0.0f, 2.0f);

                int i;
                string anim;
                GameObject atk;
                switch (currentStage)
                {
                    case BossStage.Stage1:
                        i = Random.Range(0, anim1.Length - 1);
                        anim = anim1[i];
                        animator.Play(anim, 0);

                        atk = atk1[i];
                        atk.SetActive(true);
                        atk.GetComponent<Boss_Attack>().DoAttack();
                        break;
                    case BossStage.Stage2:
                        i = Random.Range(0, anim2.Length - 1);
                        anim = anim2[i];
                        animator.Play(anim, 0);

                        atk = atk2[i];
                        atk.SetActive(true);
                        atk.GetComponent<Boss_Attack>().DoAttack();
                        break;
                    case BossStage.Stage3:
                        i = Random.Range(0, anim2.Length - 1);
                        anim = anim2[i];
                        animator.Play(anim, 0);

                        atk = atk2[i];
                        atk.SetActive(true);
                        atk.GetComponent<Boss_Attack>().DoAttack();
                        break;
                }
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStage)
        {
            case BossStage.Stage1:
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -5.4f), Time.deltaTime);
                break; 
            case BossStage.Stage2:
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -3.4f), Time.deltaTime);
                break;
            case BossStage.Stage3:
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -5.4f), Time.deltaTime);
                break;
        }
    }

    public void NextStage()
    {
        currentStage++;
        if ((int)currentStage > 2)
        {
            return;
        }
        switch (currentStage)
        {
            case BossStage.Stage1:
                break;
            case BossStage.Stage2:
                animator.Play(intro2, 0);
                atkTime = atkTime2;
                atkTimer = atkTime;
                break;
            case BossStage.Stage3:
                atkTime = atkTime3;
                atkTimer = atkTime;
                break;
        }
    }
}
