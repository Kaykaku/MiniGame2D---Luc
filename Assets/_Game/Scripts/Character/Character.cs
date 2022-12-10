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

    public float AttackRange => attackRange;
    public float FireRate => fireRate;
    public float Damage => damage;
    public float MoveSpeed => moveSpeed;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }

    public virtual void Death()
    {

    }

    public virtual void Hit()
    {

    }
}
