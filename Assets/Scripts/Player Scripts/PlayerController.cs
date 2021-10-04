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
        if (currentGun != null)
        {
            //If they are not auto
            if (!currentGun.returnAuto())
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    currentGun.OnFire();
                }
            }
            //If they are auto
            else if (currentGun.returnAuto())
            {
                if (Input.GetButton("Fire1"))
                {
                    currentGun.OnFire();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            attemptPickup();
        }
    }

    private void attemptPickup() {

        GameObject temp = inter.getPriority();
        if (temp != null)
        {
            switch (temp.tag)
            {
                case ("Gun"):
                    if (temp.GetComponent<WeaponBase>().canPickup())
                    {
                        temp.GetComponent<WeaponBase>().Onpickup();
                        setGun(temp.GetComponent<WeaponBase>());
                    }
                    break;

                case ("Gunpart"):
                    if (currentGun != null)
                    {
                        currentGun.Attach(temp.GetComponent<ModWeaponBase>());
                    }
                    break;
            }
        }
    }

    public void setGun(WeaponBase newGun) {
        if (currentGun != null) {
            currentGun.transform.position = gameObject.transform.position;
            currentGun.Drop();
        }
        currentGun = newGun;
        newGun.transform.rotation = aimer.transform.rotation;
        newGun.transform.parent = aimer.gameObject.transform;
        newGun.transform.position = aimer.Crosshair.position;
    }
    public WeaponBase getGun() {
        return currentGun;
    }
}
