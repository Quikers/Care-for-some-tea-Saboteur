using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class Draggable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Transform ParentToReturnTo = null;
        public Transform PlaceHolderParent = null;

        GameObject _placeHolder = null;

        GameObject _playerDrop;

        void Start()
        {
            _playerDrop = Resources.FindObjectsOfTypeAll< DropZone >()[ 1 ].gameObject;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            for( float t = 0; t < 1f; t += 0.1f )
            {
                transform.position = Vector3.Slerp( transform.position, new Vector3( transform.position.x, transform.position.y + 10, transform.position.z ), t );
            }

        }
        public void OnPointerExit( PointerEventData eventData )
        {
            for( float t = 0; t < 1f; t += 0.1f )
            {
                transform.position = Vector3.Lerp( transform.position, new Vector3( transform.position.x, transform.position.y - 10, transform.position.z ), t );
            }
        }

        public void OnBeginDrag( PointerEventData eventData )
        {
            if( Data.Turn.CurrentPhase == Data.TurnType.RemotePlayer || gameObject.CompareTag( "BoardCard" ) ||
                GetComponent< CardManager >().CardCost > Data.Player.CurrentEnergy )
                return;

            _placeHolder = new GameObject( "Place Holder", typeof( LayoutElement ) );
            _placeHolder.transform.SetParent( transform.parent, false );
            LayoutElement le = _placeHolder.GetComponent< LayoutElement >();
            le.preferredWidth = GetComponent< LayoutElement >().preferredWidth;
            le.preferredHeight = GetComponent< LayoutElement >().preferredHeight;

            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            _placeHolder.transform.SetSiblingIndex( transform.GetSiblingIndex() );

            ParentToReturnTo = transform.parent;
            PlaceHolderParent = ParentToReturnTo;
            transform.SetParent( Utilities.Find.InParents< Canvas >( gameObject ).transform );

            GetComponent< CanvasGroup >().blocksRaycasts = false;
            _playerDrop.SetActive( true );
        }

        public void OnDrag( PointerEventData eventData )
        {
            if( Data.Turn.CurrentPhase == Data.TurnType.RemotePlayer || gameObject.CompareTag( "BoardCard" ) ||
                GetComponent<CardManager>().CardCost > Data.Player.CurrentEnergy )
                return;
            
            RectTransform rt = GetComponent<RectTransform>();

            Vector3 globalMousePos;

            if( RectTransformUtility.ScreenPointToWorldPointInRectangle( rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
            {
                rt.position = globalMousePos;
                rt.rotation = transform.rotation;
            }

            if( _placeHolder.transform.parent != PlaceHolderParent )
            {
                _placeHolder.transform.SetParent( PlaceHolderParent );
            }

            int newSiblingIndex = PlaceHolderParent.childCount;

            for( int i = 0; i < PlaceHolderParent.childCount; i++ )
            {
                if( transform.position.x < PlaceHolderParent.GetChild( i ).position.x )
                {
                    newSiblingIndex = i;

                    if( _placeHolder.transform.GetSiblingIndex() < newSiblingIndex )
                        newSiblingIndex--;

                    break;
                }
            }
             _placeHolder.transform.SetSiblingIndex( newSiblingIndex );
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            if( Data.Turn.CurrentPhase == Data.TurnType.RemotePlayer || gameObject.CompareTag( "BoardCard" ) )
                return;

            if( GetComponent< CardManager >().CardCost > Data.Player.CurrentEnergy &
                !gameObject.CompareTag( "almostBoardCard" ) )
                return;

            transform.SetParent( ParentToReturnTo, true );
            transform.SetSiblingIndex( _placeHolder.transform.GetSiblingIndex() );
            GetComponent< CanvasGroup >().blocksRaycasts = true;

            if( gameObject.CompareTag( "almostBoardCard" ) )
            {
                gameObject.tag = "BoardCard";
                Debug.Log( "Sibling Index: " + transform.GetSiblingIndex() );

                GameObject coverImage = new GameObject( "cover", typeof( Image ), typeof( CanvasGroup ), typeof( CardAttackController ) );
                coverImage.transform.SetParent( transform, false );
                coverImage.GetComponent<Image>().rectTransform.anchorMin = Vector2.zero;
                coverImage.GetComponent<Image>().rectTransform.anchorMax = Vector2.one;
                coverImage.GetComponent<Image>().rectTransform.sizeDelta = Vector2.zero;
                coverImage.GetComponent<CanvasGroup>().alpha = 0;
                
                Destroy( _placeHolder );

                // Detroy this script
                Destroy( this );
            }


            _playerDrop.SetActive( false );
        }
    }
}