using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlotManager : MonoBehaviour
{
    private Sprite image;
    private GameObject slotItem;
    public GameObject itemPrefab;
    
    //This is run whenever the slot is occupied gets a new image to use
    public void fillSlot(GameObject item) {
        image = gameObject.GetComponent<SpriteRenderer>().sprite;
        if (slotItem == null) {
            slotItem = Instantiate(itemPrefab);
            itemPrefab.GetComponent<ItemManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
