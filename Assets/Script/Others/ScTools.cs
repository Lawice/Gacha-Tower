using UnityEngine;

namespace TD.Tools {
    public class ScTools {
        public static int RoundToNearest(float number, int nearest) {
            return Mathf.RoundToInt(number / nearest) * nearest;
        }
    }
}