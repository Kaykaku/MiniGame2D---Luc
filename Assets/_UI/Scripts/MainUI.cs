using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : FastSingleton<MainUI>
{
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI amorText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private TextMeshProUGUI fireRateText;
    [SerializeField] private Image joyStickButton;
    [SerializeField] private TextMeshProUGUI AText;
    [SerializeField] private TextMeshProUGUI DText;
    [SerializeField] private TextMeshProUGUI WText;
    [SerializeField] private TextMeshProUGUI SText;
    private Vector3 moveDir;

    private void Update()
    {
        moveDir = joyStickButton.rectTransform.anchoredPosition.normalized;
        ChangeColor(moveDir.x < 0 , AText);
        ChangeColor(moveDir.x > 0 , DText);
        ChangeColor(moveDir.y < 0 , SText);
        ChangeColor(moveDir.y > 0 , WText);
        
    }

    public void ChangeColor(bool check, TextMeshProUGUI tm)
    {
        if (check)
        {
            tm.color = Color.cyan;
        }
        else
        {
            tm.color = Color.white;
        }
    }

    public void Setting()
    {
        GameManager.Ins.Pause();
    }

    public void LoadText()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        int count =0;
        atkText.text ="ATK: "+ player.Damage ;
        rangeText.text = "Range: " + player.AttackRange;
        speedText.text = "Speed: " + player.MoveSpeed ;
        fireRateText.text = "FireRate: " + player.FireRate ;
        foreach (Weapon weapon in player.weapons)
        {
            if (weapon.gameObject.activeInHierarchy) count++;
        }
        weaponText.text = "Weapon: " + count ;
        amorText.text = "Amor: " + player.GetComponent<Health>().Armor;
    }
}
