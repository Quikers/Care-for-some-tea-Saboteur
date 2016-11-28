using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData)
    {
        if( eventData.pointerDrag == null )
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if( d != null )
        {
            d.placeHolderParent = transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if( eventData.pointerDrag == null )
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if( d != null && d.placeHolderParent == transform )
        {
            d.placeHolderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop( PointerEventData eventData )
    {
        Debug.Log( eventData.pointerDrag.name + " was dropped on " + gameObject.name );

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if( d != null)
        {
            d.parentToReturnTo = transform;
        }
    }
}
