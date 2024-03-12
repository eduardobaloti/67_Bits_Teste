using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI levelText;
    public GameObject moneyAdviceText;


    public void SetMoneyText(int money)
    {
        moneyText.text = money.ToString();
    }

    public void SetLevel(int level)
    {
        levelText.text = "Lv: " + level.ToString();
    }
}
