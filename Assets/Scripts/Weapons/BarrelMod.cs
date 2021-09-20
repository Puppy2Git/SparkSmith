using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Barrels increase speed and range
public class Barrel : ModWeaponBase
{
    public int bulletShot;//Bullets per shot
    
    // Start is called before the first frame update
    void Awake()
    {
        type = PartType.Barrel;
    }

    // Update is called once per frame
    public new int Attribute1() {
        return bulletShot;
    }
}
