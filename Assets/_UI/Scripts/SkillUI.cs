using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI skillText;
    [SerializeField] private Image skillImage;
    [SerializeField] private float time;
    private float timer;

    public void SetInfo(Sprite skillImage, string skillText)
    {
        this.skillImage.sprite = skillImage;
        this.skillText.text = skillText;
    }

    private void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer < time) return;
        timer = 0;
        Close();
    }
}
