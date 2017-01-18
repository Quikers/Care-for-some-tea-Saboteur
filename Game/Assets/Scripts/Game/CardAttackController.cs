using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class CardAttackController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool IsAttacking;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log( "dinenggg" );
            IsAttacking = true;
        }

        public void OnDrag( PointerEventData eventData )
        {
            //Vector3 mousePos = Input.mousePosition;

            //mousePos.z = Camera.main.farClipPlane;
            //Vector3 mouseWorld = Camera.main.ScreenToWorldPoint( mousePos );

            //Debug.DrawLine( transform.position, mouseWorld, Color.blue );
            Debug.Log( "dinen" );
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            Debug.Log( "dinenwww" );
            IsAttacking = false;

        }
    }
}