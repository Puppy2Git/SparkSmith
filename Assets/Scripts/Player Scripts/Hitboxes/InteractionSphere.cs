using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionSphere : MonoBehaviour
{
    public List<GameObject> interactables = new List<GameObject>();
    public GameObject interacter;
    public float interacterOffsetY;
    // Start is called before the first frame update
    void Start()

    {
        interacter = Instantiate(interacter);
        interacter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePriority();
        ShowPriority();
    }
    //This is called every frame to update which is the closest object
    private void UpdatePriority() {
        for (int i = 0; i < interactables.Count; i++) {//Through all items in the list
            if (interactables[i].transform.parent != null) {//If the item has a parent
                interactables.Remove(interactables[i]);//Remove from list
            }
        }
        //Otherwise sort list by which is closest to the player
        interactables = interactables.OrderBy(x => Vector3.Distance(x.transform.position, gameObject.transform.position)).ToList();
    }
    //This is responsible for moving the priority icon
    private void ShowPriority() {
        if (interactables.Count > 0)//If there are at least 1 item
        {
            //Move the icon at make sure it is active
            interacter.transform.position = interactables[0].transform.position + new Vector3(interactables[0].GetComponent<Collider2D>().offset.x,0,0) + new Vector3(0, interacterOffsetY);
            interacter.SetActive(true);
        }
        else {
            //deactivate the icon
            interacter.SetActive(false);
        }
    }
    //Returns the gameobject with the most priority
    public GameObject getPriority() {
        if (interactables.Count > 0)
        {
            return interactables[0];
        }
        else {
            return null;
        }
    }

    //To add to list
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Gun" || collision.gameObject.tag == "Gunpart") && collision.gameObject.transform.parent == null && !interactables.Contains(collision.gameObject))
        {
            interactables.Add(collision.gameObject);
        }
    }
    //To remove from list
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactables.Contains(collision.gameObject)) {
            interactables.Remove(collision.gameObject);
        }
    }
}
