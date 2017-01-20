using UnityEngine;
using UnityEngine.SceneManagement;
namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
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

        public void AddToQueue()
        {
            if( Data.PlayerUser.Id == 0 ) return;
            Library.SendTcp.SendPacket( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.AddPlayerToQueue, new[] { "Username", Data.PlayerUser.Username } ), Data.Network.ServerSocket );

            Resources.FindObjectsOfTypeAll<QueueInfoController>()[ 0 ].gameObject.SetActive( true );
        }

        public void ServerListener()
        {
            GetComponent<NetworkController>().StartListener();
        }
    }
}
