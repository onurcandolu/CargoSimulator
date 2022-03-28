using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] SpawnCustomers spawnCustomers;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCustomers.inQueueCustomersList.Count > 0)
            transform.LookAt(spawnCustomers.inQueueCustomersList[0].transform.position);
    }
}
