using UnityEngine;
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
            if( !eventData.pointerDrag.transform.parent.gameObject.CompareTag( "BoardCard" ) ) return;
            CardManager attackerController = eventData.pointerDrag.GetComponent< CardAttackController >().cardData;

            cardData.Health -= attackerController.Attack;
            attackerController.Health -= cardData.Attack;

            Library.SendTcp.SendPacket(
                new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.PlayerUpdate,
                    new[] { "PlayerAction", "Attack", "AttackingMinionID", attackerController.CardId.ToString(), "TargetMinionID", cardData.CardId.ToString() } ),
                Data.Network.ServerSocket );

            Debug.Log( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.PlayerUpdate,
                    new[] { "PlayerAction", "Attack", "AttackingMinionID", attackerController.CardId.ToString(), "TargetMinionID", cardData.CardId.ToString() } ) );
        }

        public void Attack( CardManager attackingCardManager, CardManager attackedCardManager )
        {

            attackedCardManager.Health -= attackingCardManager.Attack;
            attackingCardManager.Health -= attackingCardManager.Attack;

        }
    }
}