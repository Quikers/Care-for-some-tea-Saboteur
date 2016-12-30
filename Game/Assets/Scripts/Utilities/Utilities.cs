using UnityEngine;

namespace Utilities
{
    public static class Find
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
    }
}
