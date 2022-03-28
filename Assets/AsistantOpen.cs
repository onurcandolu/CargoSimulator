using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AsistantOpen : MonoBehaviour
{

    [SerializeField] TextMeshPro remainingMoney;
    [SerializeField] GameObject Asistant;
    [SerializeField] ReciveArea ReciveArea;
    int needMoney = 100;
    int currMoney = 0;

    // Start is called before the first frame update
    void Start()
    {
        remainingMoney.text = currMoney + "/" + needMoney;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool OpenAsisstant()
    {
        if (currMoney < needMoney)
        {
            currMoney++;
            remainingMoney.text = currMoney + "/" + needMoney;
            return true;
        }
        else if (currMoney >= needMoney)
        {
            Asistant.SetActive(true);
            Destroy(gameObject);
        }

        return false;
    }
    
}
