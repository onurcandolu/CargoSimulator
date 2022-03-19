using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TillArea : MonoBehaviour
{
    public SpawnCustomers spawnCustomers;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GiveCargo();
    }

    public void GiveCargo()
    {
        // �stek listesinnden iste�ini ��kar Genel listeden �r�n� ��kar 
        var list = spawnCustomers.inQueueCustomersList;
        
        list[0].GetComponent<customerController>().ExitGame();
    }
}
