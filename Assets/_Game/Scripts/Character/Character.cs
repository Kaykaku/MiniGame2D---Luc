using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected GameObject model;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fireRate = 0.5f;
    [SerializeField] protected float damage;
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool isDeath;
    public bool IsDeath => isDeath;

    public float AttackRange => attackRange;
    public float FireRate => fireRate;
    public float Damage => damage;
    public float MoveSpeed => moveSpeed;

    //Draw Attack Range On Scene
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }

    //Call on Death
    public virtual void Death()
    {

    }

    //Call in Death() and delay seconds
    protected virtual IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(1f) ;
    }

    //Call on take damage
    public virtual void Hit()
    {

    }
}
