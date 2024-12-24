using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] Text moneyTxt;

    void Start()
    {
        MinigameScript.onMoneyChanged += onMoneyChanged;
        moneyTxt.text = StaticData.money.ToString();
    }
    private void onMoneyChanged(int money)
    {
        moneyTxt.text = money.ToString();
    }
}