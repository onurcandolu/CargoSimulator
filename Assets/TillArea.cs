using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TillArea : MonoBehaviour
{
    public SpawnCustomers spawnCustomers;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(spawnCustomers.inQueueCustomersList.Count > 0)
            GiveCargo();
    }

    public void GiveCargo()
    {
        var carryItem = playerController.currItem;
        var list = spawnCustomers.inQueueCustomersList;
        customerController customer = list[0].GetComponent<customerController>();

        if (carryItem != null)
        {
            var wishCargo = customer.wishList;
            if (isCargoSame(carryItem, wishCargo))
            {
                playerController.itemList.Remove(carryItem);
                spawnCustomers.wishList.Remove(wishCargo);
                playerController.unCarrying();
                customer.ExitGame();
            }

        }
        else
        {
            customer.ShowMessage();
        }
    }

    bool isCargoSame(CargoInfo carryItem, CargoInfo wishCargo) 
                      => carryItem.section == wishCargo.section &&
                         carryItem.region == wishCargo.region &&
                         carryItem.shelf == wishCargo.shelf;
}
