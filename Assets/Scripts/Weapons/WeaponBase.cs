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
    

    private float fireTimer;//Fire delay

    // Start is called before the first frame update
    void Start()
    {
        fireTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
    }

    public void OnFire() {//Called given the input of PlayerController
        if (fireTimer >= fireRate) {//If any delay
            fireTimer = 0;
            for (int i = 0; i < bulletShot; i++) {
                aimer.Fire(bulletSpread);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {//If they are the player
            collision.gameObject.GetComponent<PlayerController>().setGun(this, auto);//Sets this as the new gun
            gameObject.GetComponent<SpriteRenderer>().enabled = false;//Disable Renderer
            gameObject.GetComponent<BoxCollider2D>().enabled = false;//
        }
    }
}
