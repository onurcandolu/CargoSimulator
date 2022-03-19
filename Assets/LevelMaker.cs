using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    [SerializeField] GameObject newLevel;
    [SerializeField] GameObject nextLevelArea;
    [SerializeField] MoneyController money;
    [SerializeField] TextMeshPro remainingMoney;
    int nextPoint = 6;
    int newLevelPoint;
    int currLevel=1;
    int needMoney = 100;
    int currMoney = 0;

    // Start is called before the first frame update
    void Start()
    {
        newLevelPoint = nextPoint;
        remainingMoney.text = currMoney + "/" + needMoney * currLevel;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextLevel()
    {
        currMoney = 0;
        nextLevelArea.transform.position += new Vector3(0, 0, -nextPoint);
        var nextLevel = Instantiate(newLevel, new Vector3(0, 0, -newLevelPoint), new Quaternion(0, 180, 0, 0));
        nextLevel.GetComponentInChildren<ReciveArea>().section = ++currLevel;
        newLevelPoint +=6;
        remainingMoney.text = currMoney + "/" + needMoney * currLevel;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Next Level") && currMoney < needMoney * currLevel && money.getMoney() > 0)
        {
            money.decreaseMoney(1);
            currMoney++;
            remainingMoney.text = currMoney + "/" + needMoney * currLevel;
        }
        else if (currMoney >= needMoney * currLevel)
            nextLevel();
    }
}
