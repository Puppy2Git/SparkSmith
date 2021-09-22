using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<ModWeaponBase> attachments;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Gun";
        fireTimer = fireRate;
        canPick = true;
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
        gameObject.GetComponent<BoxCollider2D>().enabled = true;//
        transform.parent = null;
        dropTimer = 0f;
        canPick = false;
    }
    //Attaches a Gun Part to the gun base/Stock
    public void Attach(ModWeaponBase gunpart)
    {
        switch (gunpart.type)
        {
            case PartType.Barrel:
                //bulletShot = gunpart.Attribute1();
                break;
            case PartType.Muzzle:
                //dropTimer = 0f;
                break;
            case PartType.Payload:
                //dropTimer = 0f;
                break;
            case PartType.Sight:
                //dropTimer = 0f;
                break;
        }
        bool isEmpty = true;//If there is nothing
        for (int i = 0; i < attachments.Count; i++) {//Checks through all gun parts
            if (attachments[i] != null) {//If the part exists
                if (attachments[i].type == gunpart.type)//If they are the same
                {//If the types are the same
                    isEmpty = false;
                    attachments[i].Drop();
                    attachments[i] = gunpart;//Attach new gun
                                             //Run drop for old gun type
                }

            }
            if (isEmpty) {
                attachments.Add(gunpart);
                //Sets Parent, Rotation, Position + Offset
                gunpart.transform.parent = gameObject.transform;
                gunpart.transform.rotation = transform.rotation;
                gunpart.transform.position = transform.position + (gunpart.xOff * transform.up) + (gunpart.yOff * transform.right);

            }

        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canPick) {//If they are the player
            
            collision.gameObject.GetComponent<PlayerController>().setGun(this, auto);//Sets this as the new gun
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;//Disable Renderer
            gameObject.GetComponent<BoxCollider2D>().enabled = false;//
        }
    }
}
