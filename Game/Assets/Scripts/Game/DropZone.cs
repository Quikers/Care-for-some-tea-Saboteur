using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter( PointerEventData eventData )
        {
            if( eventData.pointerDrag == null )
                return;

            Draggable d = eventData.pointerDrag.GetComponent< Draggable >();
            if( d != null )
            {
                d.PlaceHolderParent = transform;
                d.transform.rotation = transform.rotation;
            }
        }

        public void OnPointerExit( PointerEventData eventData )
        {
            if( eventData.pointerDrag == null )
                return;

            Draggable d = eventData.pointerDrag.GetComponent< Draggable >();
            if( d != null && d.PlaceHolderParent == transform )
            {
                d.PlaceHolderParent = d.ParentToReturnTo;
            }
        }

        public void OnDrop( PointerEventData eventData )
        {
            Draggable d = eventData.pointerDrag.GetComponent< Draggable >();

            if( d == null )
                return;

            d.ParentToReturnTo = transform;
            d.transform.position = new Vector3( d.transform.position.x, d.transform.position.y, transform.position.z );

            if( gameObject.CompareTag( "Board" ) && d.gameObject.CompareTag( "HandCard" ) )
                d.gameObject.tag = "almostBoardCard";
        }
    }
}