using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class CardAttackController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool IsAttacking;

        public CardManager cardData;

        GameObject lineImage;

        void Start()
        {
            cardData = GetComponentInParent< CardManager >();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            IsAttacking = true;
            lineImage = new GameObject( "cover", typeof( UnityEngine.UI.Image ) );
            lineImage.transform.SetParent( transform );

        }

        public void OnDrag( PointerEventData eventData )
        {
            Vector3 mousePos = Input.mousePosition;

            Debug.DrawLine( transform.position, mousePos, Color.blue );

            Vector3 differenceVector = mousePos - transform.position;

            lineImage.GetComponent<RectTransform>().sizeDelta = new Vector2( differenceVector.magnitude, 1f );
            lineImage.GetComponent<RectTransform>().pivot = new Vector2( 0, 0.5f );
            lineImage.GetComponent<RectTransform>().position = transform.position;
            float angle = Mathf.Atan2( differenceVector.y, differenceVector.x ) * Mathf.Rad2Deg;
            lineImage.transform.rotation = Quaternion.Euler( 0, 0, angle );
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            IsAttacking = false;
            Destroy( lineImage );
        }
    }
}