using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] public int spawnPoint;
    private GameObject target;
    private float nextFire;
    protected EnemyState currentAnim;

    //Set Player
    //Set random moveSpeed +- 10%
    //Set death is false , run every time spawn from pool
    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        moveSpeed = Random.Range(moveSpeed * 0.9f, moveSpeed * 1.1f);
        isDeath = false;
    }

    // Update is called once per frame
    // Stop on death
    // Move to player
    // Rotation look player
    // Run Anim 'Run' and check Player in attack range
    void Update()
    {
        if (isDeath) return;
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

    //If player in attack range , hit Player and run animation 'Attack' , attack cooldown by firerate
    void HitInAttackRange()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            target.GetComponent<Health>().AddDamage(damage);
            ChangeAnim(EnemyState.Attack);
        }
    }

    //Run anim 'Hit' when take damage
    public override void Hit()
    {
        base.Hit();
        ChangeAnim(EnemyState.Hit);
    }

    //Run anim 'Death' on death
    //After 1s , back enemy to the Pool and remove it from spawner
    public override void Death()
    {
        base.Death();
        ChangeAnim(EnemyState.Death);
        Spawner.instance.ememies.Remove(gameObject);
        isDeath = true;
        StartCoroutine(OnDeath());
    }

    protected override IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(1f);
        Pool2.instance.BackToPool("Rat", gameObject);
    }

    // Function change anim for Enemy
    public void ChangeAnim(EnemyState animName)
    {
        if (currentAnim != animName && !isDeath)
        {
            animator.SetBool("Run", animName.ToString().Contains("Run"));
            animator.SetTrigger(animName.ToString());
            currentAnim = animName;
        }
    }
}
