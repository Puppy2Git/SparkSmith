using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour , IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler
{
    private RectTransform rect;
    private Canvas canvas;
    private GameObject slot;
    private Vector2 offset = new Vector2(930,-440);
    private bool beingDragged = false;
    private GameObject target;
    private GameObject item;
    private Image icon;
    private float doubleclickDelay = 0.5f;
    private float doubleclickTimer;
    private bool doubleclickWindow;
    //TODO: Done?
    public void addIcon(GameObject item) {
        this.item = item;

        if (item != null)
        {
            icon.enabled = true;
            icon.sprite = item.GetComponent<SpriteRenderer>().sprite;
        }
        else {
            icon.enabled = false;
            icon.sprite = null;
        }
    }

    public bool isFull() {
        if (item != null)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public GameObject returnItem() {
        return item;
    }

    void Update() {
        if (doubleclickWindow) {
            doubleclickTimer += Time.deltaTime;
            if (doubleclickTimer >= doubleclickDelay) {//If a double click
                doubleclickWindow = false;
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        icon = gameObject.GetComponent<Image>();
        icon.enabled = false;
        slot = gameObject.transform.parent.gameObject;
        addIcon(null);
        rect = gameObject.GetComponent<RectTransform>();
        //UGLY somewhat better
        canvas = gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<Canvas>();//Gets the canvas object
    }
    //Resets the position to the center of the slot
    public void resetPosition() {
        transform.SetParent(slot.transform, false);
        rect.anchoredPosition = new Vector2(0, 0);
        beingDragged = false;
    }
    //When it is stopped being dragged
    public void OnEndDrag(PointerEventData eventData) {
        if (target == null)
        {
            setNewPosition(slot);
            slot.GetComponent<SlotManager>().dropItem();
        }
        else if (target != slot)
        {
            target.GetComponent<SlotManager>().updatepositions(target.GetComponent<SlotManager>().position, slot.GetComponent<SlotManager>().position);

            target.transform.GetChild(0).GetComponent<ItemManager>().setNewPosition(slot);
            setNewPosition(target);
        }
        slot.GetComponent<SlotManager>().requestSelector();
        //Didn't drag into a box at all lmao
        beingDragged = false;//Stop the drag
    }

    public void setNewPosition(GameObject end)
    {
        if (end != null)//If there was a change in target
        {
            slot = end;//set the new target as the slot
        }
        transform.SetParent(slot.transform, false);//set the parent as the slot
        rect.anchoredPosition = new Vector2(0, 0);//move it to the center
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (beingDragged)//Well, yea
        {
            
            if (collision.tag == "Slot")//If it is a slot
            {
                
                target = collision.gameObject;//NEW TARGET!
            }
            else if (collision.tag == "Dump") {
                
                target = null;
                
            }
            
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        slot.GetComponent<SlotManager>().requestSelector();
        if (!doubleclickWindow)
        {
            doubleclickWindow = true;
            doubleclickTimer = 0.0f;
        }
        else {
            doubleclickWindow = false;
            slot.GetComponent<SlotManager>().requestEquip();

        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        slot.GetComponent<SlotManager>().requestSelector();
    }

    public void OnBeginDrag(PointerEventData eventData) {//When first being dragged
        target = slot;//no new target
        transform.SetParent(gameObject.transform.parent.parent.parent.transform,false);//parent is now higher up
        rect.anchoredPosition = slot.GetComponent<RectTransform>().anchoredPosition - offset;//Anchor position is moved by a fixed offset
        transform.SetAsLastSibling();//Last sib
        beingDragged = true;//Is being dragged
    }

    public void OnDrag(PointerEventData eventData)//While being dragged
    {
        
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;//Move it
        
        
    }

    // Update is called once per frame
    
}
