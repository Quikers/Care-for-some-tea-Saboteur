using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class TurnController : MonoBehaviour
    {
        public Text PhaseValueText;
        public Text TurnValueText;
        public Button NextTurnButton;

        int a;
        void Start()
        {
            Data.Turn.First = Random.Range( 0, 2 ) == 1 ? Data.TurnType.LocalPlayer : Data.TurnType.RemotePlayer;
            CurrentTurn = 1;
        }
        void Update()
        {
            // this is only for now. Will be changed later and thank god for that.
            if( CurrentPhase != Data.TurnType.RemotePlayer ) return;

            if( a >= 500)
            {
                CurrentPhase = Data.TurnType.LocalPlayer;
                PhaseValueText.text = Data.TurnType.LocalPlayer.ToString();

                a = 0;
            }
            a++;
        }

        Data.TurnType CurrentPhase
        {
            get { return Data.Turn.CurrentPhase; }
            set
            {
                Data.Turn.CurrentPhase = value;
                PhaseValueText.text = value.ToString();

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
                TurnValueText.text = value.ToString();
                Data.Turn.CurrentTurn = value;

                CurrentPhase = Data.Turn.First;
            }
        }

        public void UserEndTurn()
        {
            if( CurrentPhase == Data.TurnType.RemotePlayer ) return;

            if( Data.Turn.First == Data.TurnType.LocalPlayer )
            {
                CurrentPhase = Data.TurnType.RemotePlayer;
            }
            else
            {
                CurrentTurn += 1;
            }
        }
    }
}