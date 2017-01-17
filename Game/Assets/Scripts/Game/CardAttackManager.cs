using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class CardAttackManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag( PointerEventData eventData )
        {
            Debug.DrawLine( transform.position, Input.mousePosition, Color.blue );
        }

        public void OnEndDrag( PointerEventData eventData )
        {

        }
    }
}