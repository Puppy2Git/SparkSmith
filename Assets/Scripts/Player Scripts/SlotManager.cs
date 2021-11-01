using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlotManager : MonoBehaviour
{
    private Sprite image;
    private GameObject slotItem;
    private ItemManager item;
    private GameObject inv;
    private SelectorScript selector;
    public int position;
    public bool Equip;
    //This is run whenever the slot is occupied gets a new image to use

    public bool isFull() {
        return item.isFull();
    }

    public GameObject returnItem() {
        return item.returnItem();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void updateItem(GameObject inven) {
        inv = inven;
        item = gameObject.transform.GetChild(0).GetComponent<ItemManager>();
        
    }

    public void requestSelector() {

    }

    public void requestEquip() {
        Equip = true;
    }
    public void dropItem() {
        inv.GetComponent<InventoryManagement>().removeItem(inv.GetComponent<InventoryManagement>().getItem(position));
        addItem(null);
    }
    public void updatepositions(int pos1, int pos2) {
        inv.GetComponent<InventoryManagement>().SwapPositions(pos1, pos2);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(GameObject item)
    {
        if (item != null)
        {
            this.item.addIcon(item);
        }
        else {
            this.item.addIcon(null);
        }
    }
}
