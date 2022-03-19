using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    [SerializeField] public GameObject showCargo;
    [SerializeField] int playerSpeed = 4;
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
        if(Input.GetKeyDown(KeyCode.H))
        {
            itemList.Clear();
            unCarrying();
        }

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

        var position = transform.position + transform.forward * 0.5f;
        position.y += 1.2f;
        showCargo.SetActive(true);
    }
    public void unCarrying()
    {
        animator.SetBool("Carrying", false);
        showCargo.SetActive(false);
        currItem = null;
    }

}
