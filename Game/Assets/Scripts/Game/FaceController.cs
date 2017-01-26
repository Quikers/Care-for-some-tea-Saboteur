using Game;
using UnityEngine;
using UnityEngine.EventSystems;

public class FaceController : MonoBehaviour, IDropHandler
{
    public UnityEngine.UI.Text healthText;
    public int ID;

    int CurrentHealth
    {
        get { return Data.Player.CurrentHealth; }
        set
        {
            Data.Player.CurrentHealth = value;
            healthText.text = Data.Player.CurrentHealth.ToString();
        }
    }

    void Start()
    {}

    public void OnDrop( PointerEventData eventData )
    {
        if( !eventData.pointerDrag.transform.parent.gameObject.CompareTag( "BoardCard" ) | Data.Turn.CurrentPhase != Data.TurnType.LocalPlayer)
            return;
        if( eventData.pointerDrag.GetComponent<CardAttackController>().HasAttacked )
            return;

        eventData.pointerDrag.GetComponent<CardAttackController>().EndDrag();
        eventData.pointerDrag.GetComponent<CardAttackController>().HasAttacked = true;

        CardManager attackerController = eventData.pointerDrag.GetComponent< CardAttackController >().cardData;

        Attack( attackerController );

        Library.SendTcp.SendPacket(
            new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.PlayerUpdate,
                new[] { "PlayerAction", "Attack", "AttackingMinionID", attackerController.CardId.ToString(), "TargetMinionID", ID.ToString() } ),
            Data.Network.ServerSocket );
    }

    public void Attack(CardManager attackerController )
    {
        CurrentHealth -= attackerController.Attack;
    }
}