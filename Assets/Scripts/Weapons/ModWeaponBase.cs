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
public class ModWeaponBase : MonoBehaviour
{

    public PartType type;
    public float DropTimer;
    public float xOff;
    public float yOff;
    public float attribute1;

    public void Awake()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Gun") {
            collision.gameObject.GetComponent<WeaponBase>().Attach(this);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
    public float Attribute1() {
        return attribute1;
    }
    public void Drop() {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
