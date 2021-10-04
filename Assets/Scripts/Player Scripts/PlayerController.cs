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
    private SpriteRenderer skin;
    public float crosshairDefault = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        movement = gameObject.GetComponent<CharacterMovement>();
        location = gameObject.GetComponent<Transform>();
        skin = gameObject.GetComponent<SpriteRenderer>();
        updateCrosshair();
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
        //Pick up le gun
        if (Input.GetKeyDown(KeyCode.E))
        {
            attemptPickup();
        }

        //Change Sprite Direction
        if (aimer.Crosshair.position.x >= transform.position.x)
        {
            skin.flipX = false;

        }
        else
        {
            skin.flipX = true;
        }
    }
    //This function is called whenever the player presses the pick up key and handles the interation of what type it iteracts with
    private void attemptPickup() {

        GameObject temp = inter.getPriority();//Setting to a temp gameObject
        if (temp != null)//If there is a gameObject.
        {
            switch (temp.tag)//Instead of a long if else chain
            {
                case ("Gun")://If the character picked up a gun
                    if (temp.GetComponent<WeaponBase>().canPickup())
                    {
                        temp.GetComponent<WeaponBase>().Onpickup();
                        setGun(temp.GetComponent<WeaponBase>());
                    }
                    break;

                case ("Gunpart")://If the character picked up a gunpart
                    if (currentGun != null)//If the character has a gun
                    {
                        currentGun.Attach(temp.GetComponent<ModWeaponBase>());
                        
                    }
                    break;
            }
            updateCrosshair();//Update the distance of the crosshair.
        }
    }
    //Handles updating the crosshair dependent on what the character has
    private void updateCrosshair()
    {
        if (currentGun != null)
        {
            aimer.updateCrosshairLength(currentGun.getTotalLength() + crosshairDefault/2);
        }
        else {
            aimer.updateCrosshairLength(crosshairDefault);
        }
    }
    //Handles setting the new gun
    public void setGun(WeaponBase newGun) {
        if (currentGun != null) {
            currentGun.transform.position = gameObject.transform.position;
            currentGun.Drop();
        }
        currentGun = newGun;
        newGun.transform.rotation = aimer.transform.rotation;
        newGun.transform.parent = aimer.gameObject.transform;
        newGun.transform.position = gameObject.transform.position + aimer.transform.up * 1f;
        
    }

    //Returns gun
    public WeaponBase getGun() {
        return currentGun;
    }
}
