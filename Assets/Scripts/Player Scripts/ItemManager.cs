using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour , IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Sprite icon;
    private RectTransform rect;
    private Canvas canvas;
    private GameObject slot;
    private Vector2 offset = new Vector2(930,-440);
    private bool beingDragged = false;
    private GameObject target;

    //TODO: FIX THIS
    public void setIcon(Sprite icon) {
        if (icon == null)
        {
            this.icon = icon;
            rect = gameObject.GetComponent<RectTransform>();
            //UGLY somewhat better
            canvas = gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<Canvas>();//Gets the canvas object
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        slot = gameObject.transform.parent.gameObject;
        setIcon(null);
    }
    //Resets the position to the center of the slot
    public void resetPosition() {
        transform.SetParent(slot.transform, false);
        rect.anchoredPosition = new Vector2(0, 0);
        beingDragged = false;
    }
    //When it is stopped being dragged
    public void OnEndDrag(PointerEventData eventData) {
        if (target != null)//If there was a change in target
        {
            slot = target;//set the new target as the slot
        }
        transform.SetParent(slot.transform, false);//set the parent as the slot
        rect.anchoredPosition = new Vector2(0, 0);//move it to the center
        beingDragged = false;//Stop the drag
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (beingDragged)//Well, yea
        {
            
                if (collision.tag == "Slot" && collision.gameObject.transform.childCount == 0)//If it is a slot
                {
                    target = collision.gameObject;//NEW TARGET!
                }
            
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {//When first being dragged
        target = null;//no new target
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
