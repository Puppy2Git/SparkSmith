using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponBase : MonoBehaviour
{

    public rotateScript aimer;
    public bool auto = false;//Determins if the player needs to hold down the key or not
    public float fireRate = 0.0f;//How fast each bullet can fire
    public float bulletSpread = 0.0f;
    public int bulletShot = 1;
    private float dropTimer;
    private bool canPick;
    public float dropDelay = 2f;
    private float fireTimer;
    private float gravity = -20;
    private float hoverIntencity = 0.25f;
    private double hoverTimer;
    private double hoverSpeed = 2.5f;
    private float hoverInital;
    public Vector2 barrelOffset = new Vector2();
    public Vector2 sightOffset = new Vector2();
    
    //public List<ModWeaponBase> attachments;
    private ModWeaponBase barrel;
    private ModWeaponBase payload;
    private ModWeaponBase sight;
    private ModWeaponBase muzzle;
    private bool holding;
    private bool ground;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        gameObject.tag = "Gun";
        fireTimer = fireRate;
        ground = false;
        canPick = true;
        holding = false;
    }

    // Update is called once per frame
    //Handling picking up
    void Update()
    {
          
        fireTimer += Time.deltaTime;
        if (canPick == false && ground)
        {
            dropTimer += Time.deltaTime;
        }
        if (dropTimer >= dropDelay && ground) {
            canPick = true;
        }
    }

    //handling collision
    private void FixedUpdate()
    {
        
            if (!ground && !holding)
            {
                body.velocity = new Vector3(body.velocity.x, body.velocity.y + gravity * Time.deltaTime);

            }
            if (ground && !holding)
            {

                body.position = new Vector3(body.position.x, ((float)Math.Cos(hoverTimer * hoverSpeed) * -hoverIntencity) + hoverInital + hoverIntencity);
                hoverTimer += (double)Time.deltaTime;

            }
            //Catch

        
    }


    public void OnFire() {//Called given the input of PlayerController
        if (fireTimer >= fireRate) {//If any delay
            fireTimer = 0;
            if (barrel != null)//If there is a barrel attached
            {
                
                for (int i = 0; i <barrel.Attribute2(); i++)
                {
                    aimer.Fire(bulletSpread);
                }
            }
            else {
                for (int i = 0; i < bulletShot; i++)
                {
                    aimer.Fire(bulletSpread);
                }
            }
            
        }
    }
    public void Drop() {
        dropTimer = 0f;
        holding = false;
        hoverTimer = 0f;
        transform.parent = null;
        transform.eulerAngles = new Vector3(0, 0, -90);//Rotate by -90 or right
        ground = false;
        
    }
    //Attaches a Gun Part to the gun base/Stock
    public void Attach(ModWeaponBase gunpart)
    {
        
        switch (gunpart.type)
        {
            case PartType.Barrel:
                
                if (barrel != null)
                {//If barrel does exists
                    barrel.Drop();//Drop current part
                    
                }
                
                barrel = gunpart;//Attach new part to var
                
                break;
            case PartType.Muzzle:
                if (muzzle != null)
                {//If barrel does exists
                    muzzle.Drop();//Drop current part

                }
                muzzle = gunpart;//Attach new part to var
                break;
            case PartType.Payload:
                if (payload != null)
                {//If barrel does exists
                    payload.Drop();//Drop current part

                }
                payload = gunpart;//Attach new part to var
                break;
            case PartType.Sight:
                if (sight != null)
                {//If barrel does exists
                    sight.Drop();//Drop current part

                }
                sight = gunpart;//Attach new part to var
                break;
        }
        gunpart.attached();
        attachPartToGameworld(gunpart);//Attach to gameworld
        
        
    }
    //Runned when attaching a part to the gun its self
    private void attachPartToGameworld(ModWeaponBase part) {
        
        part.transform.parent = gameObject.transform;//Sets the part's parrent
        part.transform.rotation = transform.rotation;//Sets the part's rotation
        part.transform.position = transform.position + (barrelOffset.x * transform.up) + (barrelOffset.y * transform.right);//Sets the rotation with an offset for connecting the parts

    }
    //Returns the length of the gun plus the length of the barrel
    public float getTotalLength() {
        if (barrel != null)
        {
            return barrelOffset.x + barrel.extraOffset;
        }
        else {
            return barrelOffset.x;
        }
    }
    //returns if it can be picked up
    public bool canPickup() {
        return (canPick && !holding);
    }
    //When it is picked up
    public void Onpickup() {
        holding = true;
        canPick = false;
        ground = false;
    }
    //Gets the auto stat from it
    public bool returnAuto() {
        return auto;
    }

    //If collision with the ground
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Ground" && !ground && !holding) {
            ground = true;
            hoverInital = body.position.y;
            body.velocity = new Vector3(body.velocity.x, 0);
        }
    }
}
