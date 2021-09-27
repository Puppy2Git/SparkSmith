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
    public float xOff;
    public float yOff;
    private float dropDelay = 3f;
    private float dropTimerDelay = 0f;
    private bool canPick;

    private void Awake()
    {
        
        canPick = true;
        dropTimerDelay = 0f;
    }

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

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && (canPick))
        {//If it is touching a gun and it can be picked up
            
            if (collision.gameObject.GetComponent<PlayerController>().getGun() != null)
            {
                canPick = false;
                collision.gameObject.GetComponent<PlayerController>().getGun().Attach(this);
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            }
    }
    public abstract float Attribute1();
    public abstract int Attribute2();

    public void Drop() {
        gameObject.GetComponent<Collider2D>().enabled = true;
        dropTimerDelay = 0f;//Reset timer
    }
}
