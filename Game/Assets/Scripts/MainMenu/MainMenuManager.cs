using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {

        void Start()
        {
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
