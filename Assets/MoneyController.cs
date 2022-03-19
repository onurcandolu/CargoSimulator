using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    private int currMoney {  get; set; }
    

    void Start()
    {
        currMoney = 400;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getMoney()
    { 
        return currMoney;
    }
    public bool increaseMoney(int _money)
    {
        currMoney += _money;
        moneyText. text = currMoney.ToString();
        return currMoney == currMoney + _money ;
    }
    public bool decreaseMoney(int _money)
    {
        if(currMoney - _money >= 0)
        {
            currMoney -= _money;
            moneyText.text = currMoney.ToString();
            return currMoney == currMoney - _money;
        }
        else
            return false;
    }
}
