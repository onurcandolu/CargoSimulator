using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReciveCargoRobot : MonoBehaviour
{
    [SerializeField] dockingRobot robot;
    PlayerController playerController;
    int reciveAreaNo;
    float remainingTime;
    bool canRecive;
    bool onHold;
    bool shelfFull;
    void Start()
    { 
        playerController = FindObjectOfType<PlayerController>();
        remainingTime = robot.cargoReciveTime;
        reciveAreaNo = robot.reciveArea.section;
    }

    void Update()
    {
        if(robot.currItem == null)
            canRecive = true;


        if(playerController.itemList.Where(x => x.section == reciveAreaNo).Count() < 75 )
            robot.stay = false;
        else if(shelfFull && robot.currItem == null)
            robot.stay = true;

    }
    private bool cargoPlacementControl(int[] shelf, int[] region, out int _shelf, out int _region)
    {
        if (playerController.itemList.Where(x => x.section == reciveAreaNo).Count() < 75)
        {
            for (int i = 0; i < region.Length; i++)
            {
                for (int k = 0; k < shelf.Length; k++)
                {
                    if (playerController.itemList.Where(x => x.section == reciveAreaNo && x.shelf == shelf[k] && x.region == region[i]).Count() < 5)
                    {
                        _shelf = shelf[k];
                        _region = region[i];
                        return true;
                    }
                }
            }
        }
       
        shelfFull = true;
        Debug.Log("Not Enought Space!!");
        _shelf = 0;
        _region = 0;
        return false;
    }

    private IEnumerator Recivee()
    {
        if (robot.currItem == null)
        {
            while (remainingTime > 0 && onHold)
            {
                //Debug.Log("Cargo will take after " + remainingTime + " time");
                yield return new WaitForSeconds(1f);
                remainingTime--;
            }
            if (onHold)
            {
                var shelf = Enumerable.Range(1, 3).OrderBy(g => Guid.NewGuid()).ToArray();
                var region = Enumerable.Range(1, 5).OrderBy(g => Guid.NewGuid()).ToArray();

                int _shelf, _region;
                if (cargoPlacementControl(shelf, region, out _shelf, out _region))
                {
                    CargoInfo cargo = new CargoInfo(playerController.itemList.Count() + 1, reciveAreaNo, _shelf, _region);
                    playerController.itemList.Add(cargo);
                    robot.currItem = cargo;
                   // Debug.Log("Cargo Taked.." + cargo.shelf + " - " + cargo.region);
                    robot.Carrying();
                    robot.SetDestination((cargo.shelf*5-5) + cargo.region-1);
                }
            }
            else
                remainingTime = robot.cargoReciveTime;

            yield return null;
        }
        else
            Debug.Log("Already Have Cargo..");

        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TakeArea") && canRecive)
        {
            robot.stay = false;
            canRecive = false;
            onHold = true;
            StartCoroutine(Recivee());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TakeArea"))
        {
            remainingTime = robot.cargoReciveTime;
            onHold = false;
        }
    }
}
