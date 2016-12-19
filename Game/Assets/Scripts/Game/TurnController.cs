using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public Text PhaseValueText;
    public Text TurnValueText;
    public Button NextTurnButton;

    int a = 0;
    void Start()
    {
        Data.Turn.First = Random.Range( -1, 1 ) == 1 ? Data.TurnType.LocalPlayer : Data.TurnType.RemotePlayer;
        CurrentTurn = 1;
    }
    void Update()
    {
        Debug.Log( Data.Turn.CurrentPhase );
        if( Data.Turn.CurrentPhase != Data.TurnType.RemotePlayer ) return;

        if( a >= 500)
        {
            Data.Turn.CurrentPhase = Data.TurnType.LocalPlayer;
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
        }
    }

    int CurrentTurn
    {
        get { return Data.Turn.CurrentTurn; }
        set
        {
            TurnValueText.text = value.ToString();
            Data.Turn.CurrentTurn = value;

            PhaseValueText.text = Data.Turn.First.ToString();
            Data.Turn.CurrentPhase = Data.Turn.First;
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