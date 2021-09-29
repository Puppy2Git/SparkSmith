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

    private void UpdatePriority() {
        for (int i = 0; i < interactables.Count; i++) {
            if (interactables[i].transform.parent != null) {
                interactables.Remove(interactables[i]);
            }
        }
        interactables = interactables.OrderBy(x => Vector3.Distance(x.transform.position, gameObject.transform.position)).ToList();
    }

    private void ShowPriority() {
        if (interactables.Count > 0)
        {
            interacter.transform.position = interactables[0].transform.position + new Vector3(0, interacterOffsetY);
            interacter.SetActive(true);
        }
        else {
            interacter.SetActive(false);
        }
    }

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
