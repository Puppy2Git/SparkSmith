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
    public float dropDelay;
    private float fireTimer;//Fire delay
    //public List<ModWeaponBase> attachments;
    private ModWeaponBase barrel;
    private ModWeaponBase payload;
    private ModWeaponBase sight;
    private ModWeaponBase muzzle;
    private bool holding;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Gun";
        fireTimer = fireRate;
        canPick = true;
        holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (canPick == false)
        {
            dropTimer += Time.deltaTime;
        }
        if (dropTimer >= dropDelay) {
            canPick = true;
        }
    }

    public void OnFire() {//Called given the input of PlayerController
        if (fireTimer >= fireRate) {//If any delay
            fireTimer = 0;
            for (int i = 0; i < bulletShot; i++) {
                aimer.Fire(bulletSpread);
            }
        }
    }
    public void Drop() {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;//Disable Renderer
        //gameObject.GetComponent<BoxCollider2D>().enabled = true;//
        transform.parent = null;
        dropTimer = 0f;
        canPick = false;
        holding = false;
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
        attachPartToGameworld(gunpart);//Attach to gameworld
        
        
    }

    private void attachPartToGameworld(ModWeaponBase part) {
        
        part.transform.parent = gameObject.transform;//Sets the part's parrent
        part.transform.rotation = transform.rotation;//Sets the part's rotation
        part.transform.position = transform.position + (part.xOff * transform.up) + (part.yOff * transform.right);//Sets the rotation with an offset for connecting the parts

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canPick && !holding) {//If they are the player
            holding = true;
            collision.gameObject.GetComponent<PlayerController>().setGun(this, auto);//Sets this as the new gun
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;//Disable Renderer
            
        }
    }
}
