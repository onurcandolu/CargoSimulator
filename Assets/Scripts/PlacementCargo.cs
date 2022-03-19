using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementCargo : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] float CargoPlacementTime = 3;
    [SerializeField] SpawnCustomers SpawnCustomers;
    float remainingTime;
    int regionNo;
    int shelfNo;
    PlacementArea placementArea;

    void Start()
    {
        remainingTime = CargoPlacementTime;

    }

    void Update()
    {
        
    }
    private IEnumerator Placement()
    {
        if (controller.currItem != null)
        {
            while (remainingTime > 0  && controller.currItem.region == regionNo && controller.currItem.shelf == shelfNo)
            {
                Debug.Log("Cargo will placement after " + remainingTime + " time");
                yield return new WaitForSeconds(1f);
                remainingTime--;
            }
            if(controller.currItem != null && controller.currItem.region == regionNo && controller.currItem.shelf == shelfNo)
            {
                StartCoroutine(SpawnCustomers.spawner());
                Debug.Log("Cargo Placed..");
                placementArea.cargoPlacement();
                controller.unCarrying();
            }
            else
                remainingTime = CargoPlacementTime;

            yield return null;
        }

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shelf"))
        {
            placementArea = other.gameObject.GetComponent<PlacementArea>();
            shelfNo = placementArea.shelf;
            regionNo = placementArea.region;
            StartCoroutine(Placement());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shelf"))
        {
            placementArea = null;
            shelfNo = 0;
            regionNo = 0;
            remainingTime = CargoPlacementTime;

        }
    }
}
