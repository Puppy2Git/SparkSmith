using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private CharacterMovement movement;
    private Transform location;
    public rotateScript aimer;
    public WeaponBase currentGun;//Private
    public InteractionSphere inter;
    private SpriteRenderer skin;
    public float crosshairDefault = 2.5f;
    private InventoryManagement inventory;
    public InteractionText textbox;
    private bool inputRestriction;
    // Start is called before the first frame update
    void Start()
    {
        inputRestriction = false;
        player = gameObject;
        movement = gameObject.GetComponent<CharacterMovement>();
        location = gameObject.GetComponent<Transform>();
        skin = gameObject.GetComponent<SpriteRenderer>();
        inventory = gameObject.GetComponent<InventoryManagement>();
        updateCrosshair();
    }


    // Update is called once per frame
    void Update()
    {
        //If the input is not restricted
        if (!inputRestriction)
        {
            //Movement
            movement.setHorizontal(Input.GetAxis("Horizontal"));

            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
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
                attemptPickup(false, inter.getPriority(),false);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                attemptPickup(true, inter.getPriority(),false);
            }


            //Change Sprite Direction
            if (aimer.Crosshair.position.x >= transform.position.x)
            {
                skin.flipX = false;
                if (currentGun != null)
                {
                    currentGun.toggle_spriteFlip(false);
                }
            }
            else
            {
                skin.flipX = true;
                if (currentGun != null)
                {
                    currentGun.toggle_spriteFlip(true);
                }
            }
        }
        else
        {
            movement.setHorizontal(0f);
        }
        //Press button to toggle inventory and movement
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.toggleInventoy();
            textbox.toggleTextbox();
            inputRestriction = !inputRestriction;
        }
        //Handles checking
        if (inventory.SlotToEquip != -1)
        {
            attemptPickup(true, inventory.getItem(inventory.SlotToEquip),true);
            inventory.SlotToEquip = -1;
        }
        if (inventory.toDropGun != null) {
            if (currentGun == inventory.toDropGun) {
                setGun(null);
            }
            inventory.toDropGun = null;
        }
    }

    
    //This function is called whenever the player presses the pick up key and handles the interation of what type it iteracts with
    private void attemptPickup(bool dir, GameObject temp, bool inventorycall)
    {
        //if dir is true, go to inventory otherwise go/attemp to attach to hand if that fails go to inventory
        //Setting to a temp gameObject
        if (temp != null)//If there is a gameObject.
        {
            switch (temp.tag)//Instead of a long if else chain
            {
                case ("Gun")://If the character picked up a gun
                    temp.transform.SetParent(gameObject.transform);
                    if (!inventorycall)
                        {
                            inventory.addItem(temp, dir);
                        }
                        if (dir)
                        {
                            if (currentGun == temp.GetComponent<WeaponBase>())
                            {//If it is the same thing then to store it's self instead
                                inventory.storeItem(temp);
                                setGun(null);
                            }
                            else {
                                setGun(temp.GetComponent<WeaponBase>());
                                temp.GetComponent<WeaponBase>().equip();
                            }
                            
                        }


                    
                    break;

                case ("Gunpart")://If the character picked up a gunpart

                    if (currentGun != null)
                    {
                        temp.transform.SetParent(gameObject.transform);
                        if (dir)
                        {
                            
                            GameObject toswap = null;
                            toswap = currentGun.Attach(temp.GetComponent<ModWeaponBase>());
                            inventory.removeItem(temp);
                            if (toswap != null) {
                                inventory.addItem(toswap, false);
                            }
                            
                        }
                        else {
                            if (!inventorycall)
                            {
                                inventory.addItem(temp, false);

                            }
                        }
                    }
                    else {
                        if (!inventorycall)
                        {
                            inventory.addItem(temp, false);
                        }
                    }
                    
                    break;
            }
            updateCrosshair();//Update the distance of the crosshair.
        }
    }
    //Handles updating the crosshair dependent on what the character has
    //TODO: NEED TO FIX
    private void updateCrosshair()
    {
        if (currentGun != null)
        {
            aimer.updateCrosshairLength(currentGun.getTotalLength() + crosshairDefault / 2);
        }
        else
        {
            aimer.updateCrosshairLength(crosshairDefault);
        }
    }
    //Handles setting the new gun
    public void setGun(WeaponBase newGun)
    {

        if (newGun != null)
        {
            newGun.GetComponent<WeaponBase>().Onpickup();
            if (currentGun != null)
            {//If there is a gun
                inventory.storeItem(currentGun.gameObject);

            }

            currentGun = newGun;//Set the new gun
            currentGun.setAimer(aimer);//Give the gun the RotateScript
            newGun.transform.rotation = aimer.transform.rotation * Quaternion.AngleAxis(90, Vector3.forward);//Set it's rotation
            newGun.transform.parent = aimer.gameObject.transform;//Set it's parent
            newGun.transform.position = gameObject.transform.position + aimer.transform.up * 1f;//I's position
            newGun.transform.localScale = new Vector3(1, 1, 1);//Make sure it is 1 to 1
        }
        else {
            currentGun = null;
        }
    }

    //Returns gun
    public WeaponBase getGun()
    {
        return currentGun;
    }
}
