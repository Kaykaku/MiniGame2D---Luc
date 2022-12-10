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
