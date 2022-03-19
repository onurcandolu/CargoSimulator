using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    public GameObject carryItem;
    [SerializeField] int playerSpeed = 4;
    public List<CargoInfo> itemList;
    public CargoInfo currItem;
    [SerializeField] GameObject cargoPrefab;



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

        var position = transform.position + transform.forward * 0.5f;
        position.y += 1.2f;
        carryItem = Instantiate(cargoPrefab, position, Quaternion.identity, transform);

    }
    public void unCarrying()
    {
        animator.SetBool("Carrying", false);
        Destroy(carryItem);
        currItem = null;
    }
}
