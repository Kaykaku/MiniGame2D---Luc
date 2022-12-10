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

    private void OnEnable()
    {
        if(HPBar == null) HPBar = gameObject.GetComponentInChildren<Slider>();
        HPBar.maxValue = maxHP;
        HPBar.minValue = 0;
        HPBar.value = maxHP;
        currentHP = maxHP;
    }

    private void Update()
    {
        HPBar.value = Mathf.Lerp(HPBar.value,currentHP,4 * Time.deltaTime);
        if ((currentHP - HPBar.value) > 0.01f) HPBar.value = currentHP;
    }

    public void AddMaxHP(float hpPlus)
    {
        maxHP += hpPlus;
        currentHP = maxHP;
    }

    public void AddMaxAmor(float amorPlus)
    {
        armor += amorPlus;
    }

    private void ShowFloatingText(string text , Color color)
    {
        Vector3 spawnPos = transform.position;
        spawnPos.x = Random.Range(spawnPos.x-0.5f, spawnPos.x + 0.5f);
        GameObject textFloating = Pool2.instance.SpawnFromPool("Text");
        textFloating.transform.SetPositionAndRotation(spawnPos, Quaternion.identity);
        textFloating.GetComponentInChildren<TextMesh>().text = text;
        textFloating.GetComponentInChildren<TextMesh>().color = color;
    }

    public void AddDamage(float damage)
    {
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

    
    public void AddHeal(float healPoint)
    {
        currentHP += healPoint;

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        ShowHPBar();
    }

    public void ShowHPBar()
    {
        HPBar.value = currentHP;
    }

}
