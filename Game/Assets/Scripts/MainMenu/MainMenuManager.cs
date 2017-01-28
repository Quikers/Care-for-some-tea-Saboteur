using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        EventSystem system;

        private void Start()
        {
            system = EventSystem.current;
        } 

        private void Update()
        {
            if( Input.GetKeyDown(KeyCode.Tab))
            {
                Selectable next = system.currentSelectedGameObject.GetComponent< Selectable >().FindSelectableOnDown();
                if( next != null )
                {
                    InputField inputfield = next.GetComponent<InputField>();
                    if( inputfield != null )
                        inputfield.OnPointerClick( new PointerEventData( system ) );

                    system.SetSelectedGameObject( next.gameObject, new BaseEventData( system ) );
                }
                else Debug.Log( "next nagivation element not found." );
            }
        }

        public void LoadScene( string SceneName )
        {
            SceneManager.LoadScene( SceneName );
        }

        public void LoadScene( int SceneId )
        {
            SceneManager.LoadScene( SceneId );
        }

        public void Quit()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }

        void OnApplicationQuit()
        {
            Library.SendTcp.SendPacket( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.Logout ), Data.Network.ServerSocket );
        }

        public void ServerListener()
        {
            NetCode.NetworkController.StartListener();
        }
    }
}
