﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class EnemyCardController : MonoBehaviour, IDropHandler
    {
        CardManager cardData;

        void Start()
        {
            cardData = GetComponent< CardManager >();
        }

        public void OnDrop( PointerEventData eventData )
        {
            if( !eventData.pointerDrag.transform.parent.gameObject.CompareTag( "BoardCard" ) | Data.Turn.CurrentPhase != Data.TurnType.LocalPlayer )
                return;
            if( eventData.pointerDrag.GetComponent< CardAttackController >().HasAttacked )
                return;

            CardManager attackerController = eventData.pointerDrag.GetComponent< CardAttackController >().cardData;

            eventData.pointerDrag.GetComponent< CardAttackController >().HasAttacked = true;

            cardData.Health -= attackerController.Attack;
            attackerController.Health -= cardData.Attack;

            Library.SendTcp.SendPacket(
                new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.PlayerUpdate,
                    new[] { "PlayerAction", "Attack", "AttackingMinionID", attackerController.CardId.ToString(), "TargetMinionID", cardData.CardId.ToString() } ),
                Data.Network.ServerSocket );
        }

        public void Attack( CardManager attackingCardManager, CardManager attackedCardManager )
        {
            attackedCardManager.Health -= attackingCardManager.Attack;
            attackingCardManager.Health -= attackedCardManager.Attack;

        }
    }
}