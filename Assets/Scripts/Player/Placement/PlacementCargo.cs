using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlacementCargo : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] TextMeshPro Timer;
    [SerializeField] SpawnCustomers SpawnCustomers;
    [SerializeField] float CargoPlacementTime = 3;
    [SerializeField] float CargoUnPlacementTime = 4;
    float remainingPlaceTime;
    float remainingUnPlaceTime;
    int regionNo;
    int shelfNo;
    bool onHold;
    PlacementArea placementArea;

    void Start()
    {
        remainingPlaceTime = CargoPlacementTime;
        remainingUnPlaceTime = CargoUnPlacementTime;
    }

    void Update()
    {
        
    }
    private IEnumerator Placement()
    {
        while (remainingPlaceTime > 0 && isCargoSame(controller.currItem))
        {
            Timer.text = remainingPlaceTime.ToString();

            yield return new WaitForSeconds(1f);
            remainingPlaceTime--;
        }
        Timer.text = "";

        if (controller.currItem != null && isCargoSame(controller.currItem))
        {
            Debug.Log("Cargo Placed..");
            placementArea.cargoPlacement();
            controller.unCarrying();
        }
        else
            remainingPlaceTime = CargoPlacementTime;

        yield return null;
    }
    private IEnumerator unPlacement()
    {

       if(placementArea.PlacedCargo.Count>0)
        {
            while (remainingUnPlaceTime > 0 && onHold)
            {
                Timer.text = remainingUnPlaceTime.ToString();
                yield return new WaitForSeconds(1f);
                remainingUnPlaceTime--;
            }
            Timer.text = "";
            if (onHold)
            {
                var cargo = controller.itemList.Where(x => x.shelf == shelfNo && x.region == regionNo && x.section == placementArea.section).Take(1).ToList();
                controller.currItem = cargo[0];
                placementArea.cargoUnPlacement();
                controller.Carrying();
            }
            else
                remainingUnPlaceTime = CargoUnPlacementTime;

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shelf"))
        {
            onHold = true;
            placementArea = other.gameObject.GetComponent<PlacementArea>();
            shelfNo = placementArea.shelf;
            regionNo = placementArea.region;
            if(controller.currItem == null)
                StartCoroutine(unPlacement());
            else
                StartCoroutine(Placement());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shelf"))
        {
            StopAllCoroutines();
            onHold = false;
            placementArea = null;
            shelfNo = 0;
            regionNo = 0;
            remainingPlaceTime = CargoPlacementTime;
            remainingUnPlaceTime = CargoUnPlacementTime;
            Timer.text = "";


        }
    }

    bool isCargoSame(CargoInfo currItem) => currItem.region == regionNo && currItem.shelf == shelfNo && placementArea.section == currItem.section;
}
