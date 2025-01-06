using System;
using TD.Runtime.Tower.View;
using UnityEditor;
using UnityEngine;

namespace TD.Editor {
    [CustomEditor(typeof(ScFieldOfView))]
    public class ScFieldOfViewEditor : UnityEditor.Editor {
        private void OnSceneGUI() {
            ScFieldOfView fov = (ScFieldOfView)target;
            Handles.color = Color.red;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.ViewRadius);
            
            Vector3 viewAngleA = fov.DirectionFromAngle(-fov.ViewAngle / 2, false);
            Vector3 viewAngleB = fov.DirectionFromAngle(fov.ViewAngle / 2, false);

            Handles.color = Color.yellow;
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.ViewRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.ViewRadius);

            Handles.color = Color.green;
            foreach (Transform target in fov.VisibleTargets) {
                Handles.DrawLine(fov.transform.position, target.position);
            }
        }
    }
}

