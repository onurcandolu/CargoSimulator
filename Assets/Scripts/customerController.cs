using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class customerController : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMesh;
    [SerializeField] Animator animator;
    SpawnCustomers spawnCustomers;
    public CargoInfo wishList;
    public  List<Transform> unChangeblePath;
    Vector3 ExitGamePoint;
    List<Transform> path;
    bool isAsk;
    float distance;
    bool controll;
    Vector3 targetPath;
    void Start()
    {
        ExitGamePoint = transform.position;
        path = new List<Transform>(unChangeblePath);
        spawnCustomers = transform.parent.GetComponent<SpawnCustomers>();
        controll = true;
        animator.SetBool("Walk", true);
        targetPath = path[0].position;
        navMesh.SetDestination(targetPath);
    }

  

    void Update()
    {
        if (controll)
        {
            distance = Vector3.Distance(transform.position, targetPath);
            Controll();
        }
        /* if(pathQueue <= path.Count())
             distance = Vector3.Distance(transform.position, path[pathQueue]);
         if (0.5> distance && isAsk == false)
         {
             pathQueue++;
             if(pathQueue == 2)
             {
                 animator.SetBool("Walk", false);
                 navMesh.isStopped = true;
             }
         }
         if(navMesh.isStopped && isAsk == false)
         {
             isAsk = true;
             ShowMessage();
         }*/
    }
    private void Next()
    {
        spawnCustomers.queueCustomersPoint[path[0].position] = null;

        animator.SetBool("Walk", true);
        navMesh.isStopped = false;
        if (path.Count()>1)
            path.RemoveAt(0);
      
        targetPath = path[0].position;
        spawnCustomers.queueCustomersPoint[path[0].position] = this;
        controll = true;
        navMesh.SetDestination(targetPath);


    }
    void Controll()
    {
        if (0.8 > distance)
        {
            if (path == null)
            {
                Destroy(gameObject);
            }
            else if (path.Count() > 1)
            {
                customerController outCustomer;
                spawnCustomers.queueCustomersPoint.TryGetValue(path[1].position, out outCustomer);
                if (outCustomer == null)
                {
                    Next();
                }
                else
                {
                    controll = false;
                    animator.SetBool("Walk", false);
                    navMesh.isStopped = true;
                    spawnCustomers.queueCustomersPoint[path[0].position] = this;
                }
            }
            else
            {
                ShowMessage();
                controll = false;
                animator.SetBool("Walk", false);
                navMesh.isStopped = true;
            }
        
        }
    }
    public void ExitGame()
    {
      
        spawnCustomers.queueCustomersPoint[path[0].position] = null;
        spawnCustomers.inQueueCustomersList.Remove(gameObject);

        animator.SetBool("Walk", true);
        navMesh.isStopped = false;
        path = null;

        targetPath = ExitGamePoint;
        controll = true;
        navMesh.SetDestination(targetPath);
        foreach (var customer in spawnCustomers.inQueueCustomersList)
        {
            customer.GetComponent<customerController>().Next();
        }


    }

    private void ShowMessage()
    {
            Debug.Log(wishList.shelf + " - " + wishList.region + " Cargo is mine Want it.");
    }
}
