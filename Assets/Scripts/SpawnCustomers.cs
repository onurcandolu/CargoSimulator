using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnCustomers : MonoBehaviour
{
    PlayerController controller;
    [SerializeField] List<GameObject> customers;
    [SerializeField] ReciveArea ReciveArea;
    public List<GameObject> inQueueCustomersList;
    int QueueWillBe;
    public List<Transform> path;
    public Dictionary<Vector3, customerController> queueCustomersPoint;
    int QueueLimit = 4;
    public List<CargoInfo> wishList;
   
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
        queueCustomersPoint = new Dictionary<Vector3, customerController>();
        foreach (var p in path)
            queueCustomersPoint.Add(p.transform.position, null);
        inQueueCustomersList = new List<GameObject>();
        wishList = new List<CargoInfo>();
        InvokeRepeating("spawnEachSecond",1,2);
    }

    void Update()
    {

    }

    void spawnEachSecond()
    {
        if(canSpawnCustomer())
            StartCoroutine(spawner());
    }

    public IEnumerator spawner()
    {
        if(isQueueNotFull())
        {
            customerWillCome();
            var rand = Enumerable.Range(0, customers.Count()).OrderBy(x => Guid.NewGuid()).Take(1).ToList();
            var spawnSec = Enumerable.Range(4, 30).OrderBy(x => Guid.NewGuid()).Take(1).ToList();
            yield return new WaitForSeconds(spawnSec[0]);
            createCustomer(rand);
        }
    }

    public void customerWillExit()
    {
        QueueWillBe--;
    }

    void customerWillCome()
    {
        QueueWillBe++;
    }
    void createCustomer(List<int> rand)
    {
        var customer = Instantiate(customers[rand[0]], transform.position, Quaternion.identity, transform);
        var customerContoller = customer.GetComponent<customerController>();
        var cargo = controller.itemList;
        var random = Enumerable.Range(0, cargo.Count()).
                                    Where(x => !wishList.Contains(cargo[x]) && cargo[x].section == ReciveArea.section).
                                    OrderBy(x => Guid.NewGuid()).Take(1).ToList();
        CargoInfo cargoInfo = cargo[random[0]];
        wishList.Add(cargoInfo);
        customerContoller.unChangeblePath = path;
        customerContoller.wishList = cargoInfo;
        inQueueCustomersList.Add(customer);
    }
    private bool canSpawnCustomer() => isQueueNotFull()
                                    && QueueWillBe < controller.itemList.Where(x => x.section == ReciveArea.section).Count();
    private bool isQueueNotFull() => QueueWillBe < QueueLimit;

}
