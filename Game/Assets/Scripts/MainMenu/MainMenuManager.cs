using UnityEngine;
using UnityEngine.SceneManagement;

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
}