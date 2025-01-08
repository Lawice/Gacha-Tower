using UnityEngine;

namespace TD.Runtime.Tools {
    public static class ScTools {
        public static int RoundToNearest(float number, int nearest) {
            return Mathf.RoundToInt(number / nearest) * nearest;
        }
    }
}