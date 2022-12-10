using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool2 : FastSingleton<Pool2>
{
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    protected override void Awake()
    {
        base.Awake();
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i =0; i <pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab,this.transform);
                obj.name = pool.tag + i;
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            poolDictionary.Add(pool.tag,objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Not Contain Pool");
            return null;
        }

        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            if (!poolDictionary[tag][i].activeInHierarchy)
            {
                poolDictionary[tag][i].SetActive(true);
                return poolDictionary[tag][i];
            }
        }
        Debug.Log("Out of pool " + tag);
        return null;
    }

    public void BackToPool(string tag,GameObject go)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Not Contain Pool");
            return;
        }
        go.SetActive(false);
        go.transform.parent = this.transform;
    }
}
