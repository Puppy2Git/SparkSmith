using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Barrels increase speed and range
public class BarrelMod : ModWeaponBase
{

    public int bulletShot;//Bullets per shot

    // Start is called before the first frame update
    private void Start()
    {
        type = PartType.Barrel;
    }


    // Update is called once per frame
    public override float Attribute1() {
        return 0f;
    }
    public override int Attribute2()
    {
        return bulletShot;
    }

}
