using UnityEngine;

namespace TD.Runtime.Tools {
    public static class ScTools {
        public static int RoundToNearest(float number, int nearest) {
            return Mathf.RoundToInt(number / nearest) * nearest;
        }
        
        /// <summary>
        /// Tente de trouver un composant spécifique sur le parent de l'objet donné.
        /// </summary>
        /// <typeparam name="T">Type du composant à chercher.</typeparam>
        /// <param name="child">Le GameObject enfant.</param>
        /// <param name="component">Le composant trouvé, s'il existe.</param>
        /// <returns>True si le composant est trouvé, sinon False.</returns>
        public static bool TryGetComponentInParent<T>(this GameObject child, out T component) where T : Component
        {
            component = null;
            if (child.transform.parent != null)
            {
                return child.transform.parent.TryGetComponent(out component);
            }
            return false;
        }
    }
}