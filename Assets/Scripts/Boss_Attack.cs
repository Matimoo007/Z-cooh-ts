using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss_Attack : MonoBehaviour
{
    private void Start()
    {
        
    }

    public abstract void Attack();

    protected void AttackOver()
    {
        gameObject.SetActive(false);
    }
}
