using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    [SerializeField] float timeExistence;
    [SerializeField] LayerMask layerMaskTarget;
    private float damage;
    private float timer =0;
    private Vector2 prevPos;
    private Vector2 direction;


    // Update is called once per frame
    void Update()
    {
        SelfDestroyAfter();
        MoveToHitTarget();
        DetectCollision();
    }

    public void SetDirection(Vector2 dir, float damage)
    {
        this.direction = dir;
        this.damage = damage;
    }

    public void MoveToHitTarget()
    {
        prevPos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction,Time.deltaTime *speedBullet);
    }
    public void DetectCollision()
    {
        Vector2 moveDirection = (Vector2)transform.position - prevPos;
        float moveDistance = Vector2.Distance(prevPos, transform.position) ;
        Debug.DrawRay(transform.position, moveDirection * moveDistance);
        RaycastHit2D hit = Physics2D.Raycast(prevPos, moveDirection, moveDistance ,layerMaskTarget);
        if (hit)
        {
            hit.collider.transform.GetComponent<Health>().AddDamage(damage);
            hit.collider.transform.GetComponent<Character>().Hit();
            Pool2.instance.BackToPool("Bullet", gameObject);
        }
    }

    public void SelfDestroyAfter()
    {
        timer += Time.deltaTime;
        if (timer < timeExistence) return;
        timer = 0;
        Pool2.instance.BackToPool("Bullet",gameObject);
    }


}
