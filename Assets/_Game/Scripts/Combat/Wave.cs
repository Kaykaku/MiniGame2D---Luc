using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : FastSingleton<Wave>
{
    [SerializeField] private int wave = 0;

    public int CurrentWave => wave;

    public void OnInit()
    {
        wave++;
        if(wave > 6)
        {
            GameManager.Ins.Win();
            return;
        }
        Spawner.instance.spawnPoint = Mathf.Clamp(wave * 5 + 5, 10, 50);
        Spawner.instance.spawnRate = Mathf.Clamp(1.5f - wave * 0.1f, 0.5f, 2f);
        Spawner.instance.maxLevel = Mathf.Clamp(wave, 1, 6);
    }
}
