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
    public GameObject prefab;
    public float DropTimer;
    public float xOff;
    public float yOff;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Gun") {
            collision.gameObject.GetComponent<WeaponBase>().Attach(this);
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
    public int Attribute1() {
        return 0;
    }
}
