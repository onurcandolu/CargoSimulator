using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject showCargo;
    [SerializeField] int playerSpeed = 4;
    [SerializeField] GameObject carryingCargo;
    private CharacterController controller;
    private Animator animator;
    public List<CargoInfo> itemList;
    public CargoInfo currItem;

    private void Start()
    {
        itemList = new List<CargoInfo>();
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
       
    }
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

         if (move != Vector3.zero)
        {
            animator.SetBool("Walk", true);
            controller.Move(move * Time.deltaTime * playerSpeed);
            gameObject.transform.forward = move;
        }
        else
            animator.SetBool("Walk", false);

    }

    public void Carrying()
    {
        animator.SetBool("Carrying", true);
        showCargo.SetActive(true);
        carryingCargo.gameObject.SetActive(true);
        carryingCargo.GetComponentInChildren<TextMeshProUGUI>().text = currItem.section + "-" + currItem.shelf + "-" + currItem.region ;
    }
    public void unCarrying()
    {
        animator.SetBool("Carrying", false);
        showCargo.SetActive(false);
        currItem = null; 
        carryingCargo.gameObject.SetActive(false);

    }

}
