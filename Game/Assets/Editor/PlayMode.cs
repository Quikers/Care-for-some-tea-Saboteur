using UnityEditor;
using UnityEditor.SceneManagement;

public class PlayMode
{

    [ MenuItem( "Edit/Play-Stop, But From Prelaunch Scene %0" ) ]
    public static void PlayFromPrelaunchScene()
    {
        if( EditorApplication.isPlaying == true )
        {
            EditorApplication.isPlaying = false;
            return;
        }
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene( "Assets/mainmenu.unity" );
        EditorApplication.isPlaying = true;
    }
}