using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class CardAttackController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool IsAttacking;

        public CardManager cardData;

        void Start()
        {
            cardData = GetComponentInParent< CardManager >();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            IsAttacking = true;
        }

        public void OnDrag( PointerEventData eventData )
        {
            //Vector3 mousePos = Input.mousePosition;

            //mousePos.z = Camera.main.farClipPlane;
            //Vector3 mouseWorld = Camera.main.ScreenToWorldPoint( mousePos );

            //Debug.DrawLine( transform.position, mouseWorld, Color.blue );
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            IsAttacking = false;

        }
    }
}