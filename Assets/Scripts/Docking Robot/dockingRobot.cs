using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class dockingRobot : MonoBehaviour
{
    public ReciveArea reciveArea;
    [SerializeField] GameObject stayArea;
    [SerializeField] NavMeshAgent navMesh;
    [SerializeField] GameObject showCargo;
    [SerializeField] List<GameObject> shelfList;
    public CargoInfo currItem;
    public float cargoReciveTime;
    public float cargoPlacementTime;
    int newDestination;
    public bool stay;

    void Start()
    {
    }

    void Update()
    {
        if(stay)
            navMesh.SetDestination(stayArea.transform.position);
        else if (currItem == null)
            navMesh.SetDestination(reciveArea.transform.position);
        else
            navMesh.SetDestination(shelfList[newDestination].transform.position);
    }
   

    public void Carrying()
    {
        showCargo.SetActive(true);
    }

    public void unCarrying()
    {
        showCargo.SetActive(false);
        currItem = null;
    }

    public void SetDestination(int index)
    {
        newDestination = index;
    }
   
}
