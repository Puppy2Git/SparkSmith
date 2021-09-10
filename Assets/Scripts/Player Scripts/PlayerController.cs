using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private CharacterMovement movement;
    private Transform location;
    public rotateScript aimer;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        movement = gameObject.GetComponent<CharacterMovement>();
        location = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        movement.setHorizontal(Input.GetAxis("Horizontal"));

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            movement.Dash();
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.Jump();
        }

        //Fire le gun
        if (Input.GetButtonDown("Fire1"))
        {
            aimer.Fire();
        }
    }
}
