using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class TurnController : MonoBehaviour
    {
        public Text PhaseValueText;
        public Text TurnValueText;
        public Button NextTurnButton;

        void Start()
        {
            CurrentTurn = 1;
        }

        private void Update()
        {
            if( NetCode.NetworkController.EndTurn )
            {
                if( Data.Turn.First == Data.TurnType.RemotePlayer )
                    CurrentPhase = Data.TurnType.LocalPlayer;
                else
                    CurrentTurn += 1;

                NetCode.NetworkController.EndTurn = false;
            }
        }

        Data.TurnType CurrentPhase
        {
            get { return Data.Turn.CurrentPhase; }
            set
            {
                Data.Turn.CurrentPhase = value;
                PhaseValueText.text = value == Data.TurnType.LocalPlayer ? Data.PlayerUser.Username : Data.EnemyUser.UserName;

                if( value == Data.TurnType.LocalPlayer )
                {
                    FindObjectOfType< DeckManager >().DrawCard();
                    NextTurnButton.interactable = true;
                }
                else
                    NextTurnButton.interactable = false;
            }
        }

        int CurrentTurn
        {
            get { return Data.Turn.CurrentTurn; }
            set
            {
                Data.Turn.CurrentTurn = value;
                TurnValueText.text = Data.Turn.CurrentTurn.ToString();

                CurrentPhase = Data.Turn.First;
            }
        }

        public void UserEndTurn()
        {
            if( CurrentPhase == Data.TurnType.RemotePlayer ) return;

            if( Data.Turn.First == Data.TurnType.LocalPlayer )
                CurrentPhase = Data.TurnType.RemotePlayer;
            else
                CurrentTurn += 1;

            Library.SendTcp.SendPacket( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.PlayerUpdate, new[] { "PlayerAction", "EndTurn" } ), Data.Network.ServerSocket );

        }
    }
}