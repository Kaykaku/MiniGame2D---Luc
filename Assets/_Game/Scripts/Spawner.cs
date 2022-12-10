using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : FastSingleton<Spawner>
{
    [SerializeField] public float spawnRate;
    [SerializeField] public int spawnPoint;
    [SerializeField] public int maxLevel;
    public List<GameObject> ememies = new List<GameObject>();
    private float timer;

    private void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnPoint <= 0 && ememies.Count == 0)
        {
            GameManager.Ins.StartWave();
        }
        if (timer < spawnRate || spawnPoint <= 0) return;
        timer = 0;
        Spawn();
    }
    
    public void Spawn()
    {
        GameObject enemy ;
        if (maxLevel == 6) enemy = Pool2.instance.SpawnFromPool(EnemyType.Slime.ToString());
        else enemy = Pool2.instance.SpawnFromPool(((EnemyType)Random.Range(1, maxLevel)).ToString());
        if (enemy == null) return;
        spawnPoint -= enemy.GetComponent<Enemy>().spawnPoint;
        enemy.transform.parent = transform;
        enemy.transform.position = new Vector2(Random.Range(-12f, 12f), Random.Range(-8f, 8f));
        ememies.Add(enemy);
    }
}
