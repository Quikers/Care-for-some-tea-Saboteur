using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject CardCamera;
        public Transform CardArea;

        public void OnPointerEnter( PointerEventData eventData )
        {
            if( eventData.pointerDrag == null )
                return;

            Draggable d = eventData.pointerDrag.GetComponent< Draggable >();

            if( d == null ) return;

            d.PlaceHolderParent = CardArea;
            d.transform.rotation = CardArea.rotation;

            //if( eventData.pointerDrag != null )
                //CardCamera.SetActive( true );

        }

        public void OnPointerExit( PointerEventData eventData )
        {

            if( eventData.pointerDrag == null )
                return;

            Draggable d = eventData.pointerDrag.GetComponent< Draggable >();

            if( d == null || d.PlaceHolderParent != CardArea ) return;

            //CardCamera.SetActive( false );

            d.PlaceHolderParent = d.ParentToReturnTo;
        }

        public void OnDrop( PointerEventData eventData )
        {
            Draggable d = eventData.pointerDrag.GetComponent< Draggable >();

            if( d == null )
                return;

            d.ParentToReturnTo = CardArea;
            d.transform.position = new Vector3( d.transform.position.x, d.transform.position.y, CardArea.position.z );

            if( CardArea.gameObject.CompareTag( "Board" ) && d.gameObject.CompareTag( "HandCard" ) )
            {
                d.gameObject.tag = "almostBoardCard";
            }
        }
    }
}