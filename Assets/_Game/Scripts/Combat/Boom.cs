using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private float dame;
    [SerializeField] private float range;

    //increase in size corresponding to explosions
    private void OnEnable()
    {
        transform.localScale= new Vector3(range,range,range);
    }

    ////When detonated, will deal damage to all characters in range
    private void OnDisable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range * 2);
        foreach (Collider2D collider in colliders)
        {
            Health characterHealth = collider.GetComponent<Health>();
            if(characterHealth != null)
            {
                characterHealth.AddDamage(dame);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
