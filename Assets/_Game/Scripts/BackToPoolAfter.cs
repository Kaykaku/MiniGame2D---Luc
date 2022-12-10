using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToPoolAfter : MonoBehaviour
{
    [SerializeField] private float timeExist = 1f;
    [SerializeField] private string tag;
    private void OnEnable()
    {
        StartCoroutine(BackToPool());
    }

    IEnumerator BackToPool()
    {
        yield return new WaitForSeconds(timeExist);
        Pool2.instance.BackToPool(tag,gameObject);
    }
}
