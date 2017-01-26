using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class CardAttackController : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        public bool HasAttacked;

        public CardManager cardData;

        GameObject lineImage;

        void Start()
        {
            cardData = GetComponentInParent< CardManager >();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if( HasAttacked )
                return;

            lineImage = new GameObject( "coverPointer", typeof( UnityEngine.UI.Image ) );
            lineImage.GetComponent< UnityEngine.UI.Image >().color = Color.white;
            lineImage.transform.SetParent( FindObjectOfType< Canvas >().transform );
        }

        public void OnDrag( PointerEventData eventData )
        {
            if( HasAttacked )
                return;

            Vector3 mousePos = Input.mousePosition;

            Debug.DrawLine( transform.position, mousePos, Color.blue );

            Vector3 differenceVector = mousePos - transform.position;

            lineImage.GetComponent<RectTransform>().sizeDelta = new Vector2( differenceVector.magnitude, 2f );
            lineImage.GetComponent<RectTransform>().pivot = new Vector2( 0, 0.5f );
            lineImage.GetComponent<RectTransform>().position = transform.position;
            lineImage.GetComponent< UnityEngine.UI.Image >().raycastTarget = false;
            float angle = Mathf.Atan2( differenceVector.y, differenceVector.x ) * Mathf.Rad2Deg;
            lineImage.transform.rotation = Quaternion.Euler( 0, 0, angle );
        }

        public void EndDrag()
        {
            if( HasAttacked )
                return;

            Destroy( lineImage );
        }
    }
}