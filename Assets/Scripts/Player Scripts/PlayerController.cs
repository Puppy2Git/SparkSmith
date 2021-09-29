using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private CharacterMovement movement;
    private Transform location;
    public rotateScript aimer;
    private WeaponBase currentGun;
    public InteractionSphere inter;
    private bool auto = false;
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
        if (auto == false && currentGun != null) {
            if (Input.GetButtonDown("Fire1"))
            {
                currentGun.OnFire();
            }
        }
        else if (auto == true && currentGun != null) {
            if (Input.GetButton("Fire1"))
            {
                currentGun.OnFire();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            attemptPickup();
        }
    }

    private void attemptPickup() {

        GameObject temp = inter.getPriority();
        if (temp != null)
        {
            if (temp.tag == "Gun")
            {
                if (temp.GetComponent<WeaponBase>().canPickup())
                {
                    temp.GetComponent<WeaponBase>().Onpickup();
                    setGun(temp.GetComponent<WeaponBase>(), false);
                }
            }
        }
    }

    public void setGun(WeaponBase newGun, bool isauto) {
        if (currentGun != null) {
            Debug.Log("DROP ME!");
            currentGun.Drop();
        }
        currentGun = newGun;
        newGun.transform.rotation = aimer.transform.rotation;
        newGun.transform.parent = aimer.gameObject.transform;
        newGun.transform.position = aimer.Crosshair.position;
        auto = isauto;
    }
    public WeaponBase getGun() {
        return currentGun;
    }
}
