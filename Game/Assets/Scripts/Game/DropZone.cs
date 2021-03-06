﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Transform CardArea;

        public void OnPointerEnter( PointerEventData eventData )
        {
            if( eventData.pointerDrag == null )
                return;

            Draggable d = eventData.pointerDrag.GetComponent< Draggable >();

            if( d == null ) return;

            d.PlaceHolderParent = CardArea;
            d.transform.rotation = CardArea.rotation;
        }

        public void OnPointerExit( PointerEventData eventData )
        {

            if( eventData.pointerDrag == null )
                return;

            Draggable d = eventData.pointerDrag.GetComponent< Draggable >();

            if( d == null || d.PlaceHolderParent != CardArea ) return;

            d.PlaceHolderParent = d.ParentToReturnTo;
        }

        public void OnDrop( PointerEventData eventData )
        {
            Draggable d = eventData.pointerDrag.GetComponent< Draggable >();
            
            if( d == null )
                return;

            CardManager dCardManager = d.GetComponent<CardManager>();

            d.ParentToReturnTo = CardArea;
            d.transform.position = new Vector3( d.transform.position.x, d.transform.position.y, CardArea.position.z );

            Library.Packet p = new Library.Packet( Data.PlayerUser.Id.ToString(), "Server",
                Library.TcpMessageType.PlayerUpdate,
                new[]
                {
                    "PlayerAction", "PlayCard",
                    "CardType", "Minion",
                    "Health", dCardManager.health.ToString(),
                    "Attack", dCardManager.attack.ToString(),
                    "EffectID", dCardManager.effect.id.ToString(),
                    "EnergyCost", dCardManager.cardCost.ToString(),
                    "EffectType", dCardManager.effect.type,
                    "Effect", dCardManager.effect.effect,
                    "CardID", dCardManager.CardId.ToString(),
                    "CardName", dCardManager.Cardname
                }
            );

            dCardManager.DoEffect();

            if( CardArea.gameObject.CompareTag( "Board" ) )
            {
                Debug.Log( p );
                Library.SendTcp.SendPacket( p, Data.Network.ServerSocket );
                FindObjectOfType< EnergyController >().UseEnergy( dCardManager.cardCost );
            }

            if( CardArea.gameObject.CompareTag( "Board" ) && d.gameObject.CompareTag( "HandCard" ) )
            {
                d.gameObject.tag = "almostBoardCard";
            }
        }
    }
}