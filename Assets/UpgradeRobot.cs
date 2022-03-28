using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UpgradeRobot : MonoBehaviour
{
    [SerializeField] ReciveArea reciveArea;
    [SerializeField] MoneyController money;
    [SerializeField] GameObject upgradeUI;
    [SerializeField] dockingRobot robot;
    [SerializeField] Text speedButton;
    [SerializeField] Text reciveButton;
    [SerializeField] Text placementButton;

    int speedMoneyLevel = 1;
    int reciveMoneyLevel = 1;
    int placementMoneyLevel = 1;
  
    private void updateVariable()
    {
        speedButton.text = (100 * speedMoneyLevel).ToString();
        reciveButton.text = (100 * reciveMoneyLevel).ToString();
        placementButton.text = (100 * placementMoneyLevel).ToString();
    }

    public void speedButtonPresed()
    {
        if (100 * speedMoneyLevel <= money.getMoney())
            if(money.decreaseMoney(100 * speedMoneyLevel))
            {
                speedMoneyLevel += 1;
                robot.GetComponent<NavMeshAgent>().speed *= 1.1f;
            }
        updateVariable();
    }
    public void reciveButtonPresed()
    {
        if (100 * reciveMoneyLevel <= money.getMoney())
            if (money.decreaseMoney(100 * reciveMoneyLevel))
            {
                reciveMoneyLevel += 1;
                robot.cargoReciveTime *= 0.9f;
            }
        updateVariable();
    }
    public void placementButtonPresed()
    {
        if (100 * placementMoneyLevel <= money.getMoney())
            if (money.decreaseMoney(100 * placementMoneyLevel))
            {
                placementMoneyLevel += 1;
                robot.cargoPlacementTime *= 0.9f;
            }
        updateVariable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Carrier"))
        {
            updateVariable();
            upgradeUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Carrier"))
        {
            upgradeUI.SetActive(false);
        }
    }
}
