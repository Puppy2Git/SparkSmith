using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    private bool InventoryEnabled;
    public GameObject Inventory;
    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    public GameObject[] slot_items;
    public GameObject slotHolder;
    
    //Toggles the inventory
    public void toggleInventoy() {
        InventoryEnabled = !InventoryEnabled;
    }
    public void addItem(GameObject item) {
        bool done = false;
        for (int i = 0; i < allSlots; i++) {
            if (slot_items[i] == null && !done) {
                slot_items[i] = item;
                done = true;
                switch (item.tag) {
                    case ("Gun"):
                        item.GetComponent<WeaponBase>().store();
                        break;
                    case ("Gunpart"):
                        item.GetComponent<ModWeaponBase>().store();
                        break;
                }
            }
        }
        done = false;
        for (int i = 0; i < allSlots; i++) {
            if (!slot[i].GetComponent<SlotManager>().isFull() && !done) {
                slot[i].GetComponent<SlotManager>().addItem(item);
                done = true;
            }
        }
    }
    public void removeItem(GameObject item) {
        for (int i = 0; i < allSlots; i++) {
            if (slot[i].GetComponent<SlotManager>().returnItem() == item) {
                slot[i].GetComponent<SlotManager>().addItem(null);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SwapPositions(int pos1, int pos2) {
        GameObject tmp = slot_items[pos1];
        slot_items[pos1] = slot_items[pos2];
        slot_items[pos2] = tmp;

    }

    //Is called when the inventory is finished generating by SlotManager.cs
    public void initInventory() {
        allSlots = 16;
        slot = new GameObject[allSlots];
        slot_items = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
            slot[i].GetComponent<SlotManager>().updateItem(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Updates inventory active
        Inventory.SetActive(InventoryEnabled);
    }
}
