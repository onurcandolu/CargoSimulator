using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnCustomers : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] List<GameObject> customers;
    [SerializeField] ReciveArea ReciveArea;
    public List<GameObject> inQueueCustomersList;
    public List<Transform> path;
    public Dictionary<Vector3, customerController> queueCustomersPoint;
    int QueueNumber;
    public List<CargoInfo> wishList;
    // didem kesit bunu yap4
    void Start()
    {
        queueCustomersPoint = new Dictionary<Vector3, customerController>();
        foreach (var p in path)
            queueCustomersPoint.Add(p.transform.position, null);
        inQueueCustomersList = new List<GameObject>();
        wishList = new List<CargoInfo>();
    }

    void Update()
    {
       
    }

    public IEnumerator spawner()
    {
        if(QueueNumber < 4)
        {
            QueueNumber++;
            var rand = Enumerable.Range(0, customers.Count()).OrderBy(x => Guid.NewGuid()).Take(1).ToList();
            var spawnSec = Enumerable.Range(1, 1).OrderBy(x => Guid.NewGuid()).Take(1).ToList();
            yield return new WaitForSeconds(spawnSec[0]);
            createCustomer(rand);
        }
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
}
