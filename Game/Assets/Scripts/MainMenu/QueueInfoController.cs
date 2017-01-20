using UnityEngine;
namespace MainMenu
{
    public class QueueInfoController : MonoBehaviour
    {
        float time;

        public UnityEngine.UI.Text timeText;

        private void OnEnable()
        {
        }

        private void Update()
        {
            time += Time.deltaTime;
            timeText.text = Mathf.RoundToInt( time ).ToString();
        }
        private void OnDisable()
        {
            time = 0;
            timeText.text = "";
        }

        public void CancelQueue()
        {
            Library.SendTcp.SendPacket( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.CancelMatchmaking, new[] { "Username", Data.PlayerUser.Username } ), Data.Network.ServerSocket );

            gameObject.SetActive( false );
        }
    }
}
