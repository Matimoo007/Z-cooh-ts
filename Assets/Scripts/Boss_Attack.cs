using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss_Attack : MonoBehaviour
{
    public void DoAttack()
    {
        StartCoroutine("Attack");
    }

    public void StopAttack()
    {
        StopCoroutine("Attack");
    }

    protected abstract IEnumerator Attack();

    protected void AttackOver()
    {
        gameObject.SetActive(false);
    }
}
