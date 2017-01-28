using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class Find : MonoBehaviour
    {
        public static T InParents<T>( GameObject go ) where T : Component
        {
            if( go == null )
                return null;
            var comp = go.GetComponent<T>();

            if( comp != null )
                return comp;

            var t = go.transform.parent;
            while( t != null && comp == null )
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }

        public static CardManager CardById( int id )
        {
            CardManager[] cards = FindObjectsOfType< CardManager >();
            foreach( CardManager cardManager in cards )
            {
                if( cardManager.CardId == id )
                    return cardManager;
            }
            return null;
        }
    }

    public static class Screen
    {
        public static void LogError( object message )
        {
            GameObject ErrorWindow = new GameObject( "ErrorWindow" );
        }
    }
}
