using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider HPBar;
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;
    [SerializeField] private float armor;
    [SerializeField] private float currentRage;
    [SerializeField] private float maxRage;

    public float CurrentHP => currentHP;
    public float Armor => armor;

    //Set max hp value
    private void OnEnable()
    {
        if(HPBar == null) HPBar = gameObject.GetComponentInChildren<Slider>();
        HPBar.maxValue = maxHP;
        HPBar.minValue = 0;
        HPBar.value = maxHP;
        currentHP = maxHP;
    }

    //Hp decrease slowly by speed and stop when equals currentHP
    private void Update()
    {
        HPBar.value = Mathf.Lerp(HPBar.value,currentHP,4 * Time.deltaTime);
        if ((currentHP - HPBar.value) > 0.01f) HPBar.value = currentHP;
    }

    // increase maxHP value by amount and heal full HP
    public void AddMaxHP(float hpPlus)
    {
        maxHP += hpPlus;
        currentHP = maxHP;
    }

    // increase amor value by amount 
    public void AddMaxAmor(float amorPlus)
    {
        armor += amorPlus;
    }

    //Set floating text at character pos
    private void ShowFloatingText(string text , Color color)
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y += 0.5f;
        spawnPos.x = Random.Range(spawnPos.x-0.5f, spawnPos.x + 0.5f);
        GameObject textFloating = Pool2.instance.SpawnFromPool("Text");
        textFloating.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);
        textFloating.GetComponentInChildren<TextMesh>().text = text;
        textFloating.GetComponentInChildren<TextMesh>().color = color;
    }

    // Take amount of dame in range 0.2 to 100 damage, character will call Death Function when HP <=0
    // Show currentHp on bar
    public void AddDamage(float damage)
    {
        if (GetComponent<Character>().IsDeath) return;
        float damageTotal= Mathf.Clamp(damage-armor,0.2f,100);
        currentHP -= damageTotal > 0 ? damageTotal : 0;

        if(currentHP<= 0)
        {
            ShowFloatingText("Death", Color.black);
            GetComponent<Character>().Death();
        }
        ShowHPBar();
        ShowFloatingText(damageTotal.ToString(), new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f)));
    }

    //Heal character by amount
    public void AddHeal(float healPoint)
    {
        currentHP += healPoint;

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        ShowHPBar();
    }

    //Show currentHP Bar
    public void ShowHPBar()
    {
        HPBar.value = currentHP;
    }

}
