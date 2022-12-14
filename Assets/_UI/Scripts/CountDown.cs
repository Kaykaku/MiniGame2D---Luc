using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown : UICanvas
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float time;
    private float timer;


    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        GameManager.Ins.StopTime();
        text.text = Mathf.Round(time - timer).ToString();
        if (timer < time) return;
        timer = 0;
        GameManager.Ins.RunTime();
        Close();
    }
}
