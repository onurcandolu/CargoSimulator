using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementCargoRobot : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] dockingRobot robot;
    float remainingPlaceTime;
    int regionNo;
    int shelfNo;
    bool onHold;
    PlacementArea placementArea;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        remainingPlaceTime = robot.cargoPlacementTime;
    }

    void Update()
    {

    }
    private IEnumerator Placement()
    {
        while (remainingPlaceTime > 0 && isCargoSame(robot.currItem))
        {
            //Debug.Log("Cargo will placement after " + remainingPlaceTime + " time");
            yield return new WaitForSeconds(1f);
            remainingPlaceTime--;
        }
        if (robot.currItem != null && isCargoSame(robot.currItem))
        {
           // Debug.Log("Cargo Placed..");
            placementArea.cargoPlacement();
            robot.unCarrying();
        }
        else
            remainingPlaceTime = robot.cargoPlacementTime;

        yield return null;
    }
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shelf"))
        {
            onHold = true;
            placementArea = other.gameObject.GetComponent<PlacementArea>();
            if(placementArea != null)
            {

                shelfNo = placementArea.shelf;
                regionNo = placementArea.region;
                if (robot.currItem != null && isCargoSame(robot.currItem))
                    StartCoroutine(Placement());
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shelf"))
        {
            onHold = false;
            placementArea = null;
            shelfNo = 0;
            regionNo = 0;
            remainingPlaceTime = robot.cargoPlacementTime;
        }
    }

    bool isCargoSame(CargoInfo currItem) => currItem.region == regionNo && currItem.shelf == shelfNo && placementArea.section == currItem.section;
}
