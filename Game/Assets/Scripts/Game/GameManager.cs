using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public GameObject WinScreen;
        public GameObject PauseMenu;

        CardManager GetCard( int id )
        {
            return Utilities.Find.CardById( id );
        }

        public void BackToMM()
        {
            Library.SendTcp.SendPacket( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.MatchEnd ), Data.Network.ServerSocket );
            WinScreen.SetActive( false );

            UnityEngine.SceneManagement.SceneManager.LoadScene( "mainmenu" );
        }

        private void Start()
        {
            Data.Player.CurrentHealth = 20;
            Data.Enemy.CurrentHealth = 20;
        }

        public static void ActiveAllCards()
        {
            CardAttackController[] attackingCards = FindObjectsOfType< CardAttackController >();
            foreach( var attackingCard in attackingCards )
                attackingCard.HasAttacked = false;

            List< GameObject > energyObjects = FindObjectOfType< EnergyController >().EnergyObjects;
            foreach( var energyObject in energyObjects )
            {
                energyObject.SetActive( true );
            }
        }

        void Update()
        {
            if( PauseMenu.activeSelf & Input.GetKeyDown( KeyCode.Escape ) )
                PauseMenu.SetActive( false );
            else if( !PauseMenu.activeSelf & Input.GetKeyDown( KeyCode.Escape ) )
                PauseMenu.SetActive( true );

            if( Data.Player.CurrentHealth <= 0 )
            {
                WinScreen.SetActive( true );
                WinScreen.GetComponentInChildren< Text >().text = "You lost!";
            }
            else if( Data.Enemy.CurrentHealth <= 0 )
            {
                WinScreen.SetActive( true );
                WinScreen.GetComponentInChildren< Text >().text = "Congratulations You Won!";
            }

            if( NetCode.NetworkController.PlayCardsQueue.Count >= 1 )
            {
                for( int i = 0; i < NetCode.NetworkController.PlayCardsQueue.Count; i++ )
                {
                    FindObjectOfType<DeckManager>().PlayEnemyCard( NetCode.NetworkController.PlayCardsQueue[ i ] );
                    NetCode.NetworkController.PlayCardsQueue.RemoveAt( i );
                }
            }

            if( NetCode.NetworkController.AttackingQueue.Count >= 1 )
            {
                foreach( var attacker in NetCode.NetworkController.AttackingQueue.ToArray() )
                {
                    if( attacker.Key >= 0 )
                    {
                        GetCard( attacker.Value )
                            .GetComponent< EnemyCardController >()
                            .Attack( GetCard( attacker.Value ), GetCard( attacker.Key ) );
                    }
                    else if( attacker.Key == -1 )
                    {
                        FindObjectsOfType< FaceController >()[ 0 ]
                            .Attack( GetCard( attacker.Value ) );
                    }
                    else if( attacker.Key == -2 )
                    {
                        FindObjectsOfType< FaceController >()[ 1 ]
                            .Attack( GetCard( attacker.Value ) );

                    }
                    NetCode.NetworkController.AttackingQueue.Remove( attacker.Key );
                }
            }            
        }

        void OnApplicationQuit()
        {
            Library.SendTcp.SendPacket( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.MatchEnd ), Data.Network.ServerSocket );

            Library.SendTcp.SendPacket( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.Logout ), Data.Network.ServerSocket );
        }

        //void LateUpdate()
        //{
        //    Rect canvasRect = new Rect( GetComponent<RectTransform>().rect );
        //    if( !canvasRect.Contains( Input.mousePosition ) )
        //    { }
        //}
    }
}