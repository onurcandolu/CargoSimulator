using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private int currMoney {  get; set; }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool increaseMoney(int _money)
    {
        Debug.Log(_money+" + " + currMoney +"= ");
        currMoney += _money;
        Debug.Log(currMoney );
        return currMoney == currMoney + _money ;
    }
    public bool decreaseMoney(int _money)
    {
        if(currMoney - _money > 0)
        {
            currMoney -= _money;
            return currMoney == currMoney - _money;
        }
        else
            return false;
    }
}
