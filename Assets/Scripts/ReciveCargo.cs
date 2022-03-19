using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReciveCargo : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] float CargoReciveTime = 3;
    int reciveAreaNo;
    float remainingTime;
    bool onHold;

    void Start()
    {
        remainingTime = CargoReciveTime;
    }

    void Update()
    {

    }
    private bool cargoPlacementControl(int[] shelf, int[] region, out int _shelf, out int _region)
    {
         // tüm alan iþin kontrol var ama tek bir raf için kontrol 5 taneye kadar sýnýrlandýrýlmalý
        if (controller.itemList.Where(x => x.section == reciveAreaNo).Count() !=75) 
        {
            for(int i = 0; i<region.Length;i++)
            {
                for (int k = 0; k < shelf.Length; k++)
                {
                    if (controller.itemList.Where(x => x.section == reciveAreaNo && x.shelf == shelf[k] && x.region == region[i]).Count() < 5)
                    {
                        _shelf = shelf[k];
                        _region = region[i];
                        return true;
                    }
                }
            }
        }
        Debug.Log("Not Enought Space!!");
        _shelf = 0;
        _region = 0;
        return false;
    }
    private IEnumerator Recive()
    {
        if(controller.currItem == null)
        {
            while (remainingTime > 0 && onHold)
            {
                Debug.Log("Cargo will take after " + remainingTime + " time");
                yield return new WaitForSeconds(1f);
                remainingTime--;
            }
            if(onHold)
            {
                var shelf = Enumerable.Range(1, 3).OrderBy(g => Guid.NewGuid()).ToArray();  
                var region = Enumerable.Range(1, 5).OrderBy(g => Guid.NewGuid()).ToArray();

                    int _shelf,_region;
                    if (cargoPlacementControl(shelf , region, out _shelf, out _region))
                    {
                        CargoInfo cargo = new CargoInfo(controller.itemList.Count()+1,reciveAreaNo, _shelf, _region);
                        controller.itemList.Add(cargo);
                        controller.currItem = cargo;
                        Debug.Log("Cargo Taked.." + cargo.shelf + " - " + cargo.region);
                        controller.Carrying();
                    }
            }
            else
                remainingTime = CargoReciveTime;

            yield return null;
        }
        else
            Debug.Log("Already Have Cargo..");

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TakeArea"))
        {
            reciveAreaNo = other.gameObject.GetComponent<ReciveArea>().section;
            onHold = true;
            StartCoroutine(Recive());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TakeArea"))
        {
            reciveAreaNo = 0;
            remainingTime = CargoReciveTime;
            onHold = false;
        }
    }
}
