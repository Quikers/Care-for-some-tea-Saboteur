using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform ParentToReturnTo = null;
    public Transform PlaceHolderParent = null;

    GameObject _placeHolder = null;

    public void OnBeginDrag( PointerEventData eventData )
    {
        _placeHolder = new GameObject( "Place Holder" );
        _placeHolder.transform.SetParent( transform.parent, false );
        LayoutElement le = _placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight= GetComponent<LayoutElement>().preferredHeight;

        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        _placeHolder.transform.SetSiblingIndex( transform.GetSiblingIndex() );

        ParentToReturnTo = transform.parent;
        PlaceHolderParent = ParentToReturnTo;
        transform.SetParent( Utilities.FindInParents<Canvas>( gameObject ).transform );
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag( PointerEventData eventData )
    {
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
        transform.SetParent( ParentToReturnTo, true );
        transform.SetSiblingIndex( _placeHolder.transform.GetSiblingIndex() );
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy( _placeHolder );
    }
}