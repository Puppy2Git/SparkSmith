using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    Barrel,//Dictates Bullets Per Shot
    Payload,//Dictates Ammo Type/Capasity
    Sight,//Dictates accuracy/bullet spread
    Muzzle,//Speed and sound
}
public abstract class ModWeaponBase : MonoBehaviour
{

    public PartType type;
    public float DropTimer;
    private float dropDelay = 3f;
    private float dropTimerDelay = 0f;
    private bool canPick;
    public float extraOffset;
    private Rigidbody2D body;
    private double hoverTimer;
    private float hoverSpeed = 2.5f;
    private float gravity = -20f;
    private float hoverIntencity = 0.25f;
    private float hoverInital;
    private bool ground;
    private bool holding;
    private SpriteRenderer sprit;
    //Wakee Wakee
    private void Awake()
    {
        sprit = gameObject.GetComponent<SpriteRenderer>();
        body = gameObject.GetComponent<Rigidbody2D>();  
        canPick = true;
        dropTimerDelay = 0f;
        ground = false;
        holding = false;
    }

    //Handles flipping of the sprite
    public void toggle_spriteFlip(bool dir) {
        sprit.flipY = dir;
    }

    public void store() {
        gameObject.SetActive(false);
    }

    //Update Loop
    private void Update()
    {
        if (dropTimerDelay >= dropDelay && !canPick) {
            canPick = true;
        }
        else
        {
            dropTimerDelay += Time.deltaTime;
        }
    }

    //FixedUpdate Loop for gravity
    private void FixedUpdate()
    {
        //If they are in the air and are not being held
        if (!ground && !holding)
        {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y + gravity * Time.deltaTime);

        }
        //If they are on the ground
        if (ground && !holding)
        {

            body.position = new Vector3(body.position.x, ((float)Math.Cos(hoverTimer * hoverSpeed) * -hoverIntencity) + hoverInital + hoverIntencity);
            hoverTimer += (double)Time.deltaTime;

        }
        

    }
    //Called when attached to a gun
    public void attached() {
        gameObject.SetActive(true);
        holding = true;
        ground = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    //If they collide with the ground
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !ground && !holding)
        {
            ground = true;
            hoverInital = body.position.y;
            body.velocity = new Vector3(body.velocity.x, 0);
        }
    }

    //To be changed by other classes
    public abstract float Attribute1();
    public abstract int Attribute2();


    //When dropped
    public void Drop() {
        gameObject.SetActive(true);
        sprit.flipY = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
        dropTimerDelay = 0f;//Reset timer
        holding = false;
        hoverTimer = 0f;
        transform.parent = null;
        transform.eulerAngles = new Vector3(0, 0, 0);//Rotate by -90 or right
        ground = false;
    }
}
