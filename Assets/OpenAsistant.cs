using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenAsistant : MonoBehaviour
{
    [SerializeField] MoneyController money;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {// Burda sadece buna para yatýrdýgý söylensin diðer objede texti güncelleyip kontrol etsin
        if (other.CompareTag("Asistant Open") &&  money.getMoney() > 0)
        {
            var open = other.GetComponent<AsistantOpen>();
            if(open.OpenAsisstant())
                money.decreaseMoney(1);
        }
    }
}
