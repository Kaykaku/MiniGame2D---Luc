using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bonus : UICanvas
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private  List<GameObject> cards;
    private List<GameObject> randomCards = new List<GameObject>();

    private void OnEnable()
    {
        foreach (GameObject card in cards)
        {
            card.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 1000f, 0f);
        }

        waveText.text = "Wave " + Wave.instance.CurrentWave + " Reward !";
        randomCards.Clear();
        while (randomCards.Count!=3)
        {
            int rd = Random.Range(0,cards.Count);
            if (!randomCards.Contains(cards[rd]))
            {
                randomCards.Add(cards[rd]);
                cards[rd].GetComponent<RectTransform>().anchoredPosition = new Vector3(randomCards.Count * 500 - 1000, -100, 0f);
            }
        }
    }
    
    public void BonusButton(int bonus)
    {
        Time.timeScale = 1;
        GameObject.FindObjectOfType<Player>().AddBonus((BonusType) bonus);
        Close();
    }
}
