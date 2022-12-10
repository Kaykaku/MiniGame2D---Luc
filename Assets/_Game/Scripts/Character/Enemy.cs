using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] public int spawnPoint;
    private GameObject target;
    private float nextFire;
    protected EnemyState currentAnim;

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        moveSpeed = Random.Range(moveSpeed * 0.9f, moveSpeed * 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position , Time.deltaTime * moveSpeed);
        if(transform.position.x > target.transform.position.x)
        {
            model.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
        else
        {
            model.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        ChangeAnim(EnemyState.Run);
        HitInAttackRange();
    }

    void HitInAttackRange()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            target.GetComponent<Health>().AddDamage(damage);
            ChangeAnim(EnemyState.Attack);
        }
    }

    public override void Hit()
    {
        base.Hit();
        ChangeAnim(EnemyState.Hit);
    }

    public override void Death()
    {
        base.Death();
        ChangeAnim(EnemyState.Death);
        Spawner.instance.ememies.Remove(gameObject);
        Pool2.instance.BackToPool("Rat",gameObject);
    }

    public void ChangeAnim(EnemyState animName)
    {
        if (currentAnim != animName)
        {
            animator.SetBool("Run", animName.ToString().Contains("Run"));
            animator.SetTrigger(animName.ToString());
            currentAnim = animName;
        }
    }
}
