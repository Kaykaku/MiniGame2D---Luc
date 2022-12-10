using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shotPoint;

    [SerializeField] private GameObject target;
    private Player player;
    private float attackRange;
    private float fireRate ;
    private float nextFire;
    private float damage;

    private void Start()
    {
        player = transform.root.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        fireRate = player.FireRate;
        attackRange = player.AttackRange;
        damage = player.Damage;

        if (Spawner.instance.ememies.Contains(target) && Vector2.Distance(player.transform.position, target.transform.position) <= attackRange  )
        {
            LockDirectionOnTarget();
            HitInAttackRange();
        }
        else
        {
            DetectTarget();
        }

    }

    void DetectTarget()
    {
        if (Spawner.instance.ememies.Count > 0 )
        {
            target = Spawner.instance.ememies[0];
            float minDistance = Vector2.Distance(player.transform.position, target.transform.position) ;
            foreach (GameObject enemy in Spawner.instance.ememies)
            {
                float distance =Vector2.Distance(player.transform.position, enemy.transform.position);
                if (distance <= attackRange && distance < minDistance)
                {
                    target = enemy;
                }
            }
        }
    }


    void LockDirectionOnTarget()
    {
        Vector3 lookDirection = (transform.position - target.transform.position).normalized;
        model.transform.right = lookDirection;
    }

    void HitInAttackRange()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Pool2.instance.SpawnFromPool("Bullet");
            bullet.transform.SetPositionAndRotation(shotPoint.transform.position, bulletPrefab.transform.rotation);
            Vector2 dir = (Vector2)(target.transform.position - transform.position).normalized;
            bullet.GetComponent<BulletBehavior>().SetDirection(dir,damage);
        }
    }

}
