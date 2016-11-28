using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;
    public Transform placeHolderParent = null;

    GameObject placeHolder = null;

    public void OnBeginDrag( PointerEventData eventData )
    {
        Debug.Log( "OnBeginDrag" );
        placeHolder = new GameObject( "Place Holder" );
        placeHolder.transform.SetParent( transform.parent );
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight= GetComponent<LayoutElement>().preferredHeight;

        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeHolder.transform.SetSiblingIndex( transform.GetSiblingIndex() );

        parentToReturnTo = transform.parent;
        placeHolderParent = parentToReturnTo;
        transform.SetParent( transform.parent.parent );

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag( PointerEventData eventData )
    {
        transform.position = eventData.position;

        if( placeHolder.transform.parent != placeHolderParent )
            placeHolder.transform.SetParent( placeHolderParent );

        int newSiblingIndex = placeHolderParent.childCount;

        for( int i = 0; i < placeHolderParent.childCount; i++ )
        {
            if( transform.position.x < placeHolderParent.GetChild( i ).position.x )
            {
                newSiblingIndex = i;

                if( placeHolder.transform.GetSiblingIndex() < newSiblingIndex )
                    newSiblingIndex--;

                break;
            }
        }
        placeHolder.transform.SetSiblingIndex( newSiblingIndex );
    }

    public void OnEndDrag( PointerEventData eventData )
    {
        Debug.Log( "OnEndDrag" );
        transform.SetParent( parentToReturnTo );
        transform.SetSiblingIndex( placeHolder.transform.GetSiblingIndex() );
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy( placeHolder );
    }
}