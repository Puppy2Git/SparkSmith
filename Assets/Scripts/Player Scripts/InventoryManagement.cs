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
    public GameObject slotHolder;
    
    //Toggles the inventory
    public void toggleInventoy() {
        InventoryEnabled = !InventoryEnabled;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //Is called when the inventory is finished generating by SlotManager.cs
    public void initInventory() {
        allSlots = 16;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Updates inventory active
        Inventory.SetActive(InventoryEnabled);
    }
}
