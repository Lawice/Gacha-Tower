using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace TD.InputSystem{
    public class ScInputManager : MonoBehaviour {
        public static ScInputManager Instance { get; private set; }
        
        public InputEvent OnMoveEvent;
        public Vector3 MoveValue;
        public bool IsMoving;

        public InputEvent OnViewEvent;
        public Vector2 ViewValue;
        public bool IsViewing;
        
        public InputEvent OnInteractEvent;
        public bool IsInteracting;
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        public void OnMove(InputAction.CallbackContext ctx) {
            MoveValue = ctx.ReadValue<Vector3>();
            InvokeInputEvent(ctx, OnMoveEvent);
        }
        public void OnInteract(InputAction.CallbackContext ctx) {
            InvokeInputEvent(ctx, OnInteractEvent);
        }

        public void OnView(InputAction.CallbackContext ctx) {
            ViewValue = ctx.ReadValue<Vector2>();
            InvokeInputEvent(ctx, OnViewEvent);
        }

        private void InvokeInputEvent(InputAction.CallbackContext ctx, InputEvent inputEvent) {
            if (ctx.started) {
                inputEvent.Started?.Invoke();
            } else if (ctx.performed) {
                inputEvent.Performed?.Invoke();
            } else if (ctx.canceled) {
                inputEvent.Canceled?.Invoke();
            }
        }
        

        
        
        [Serializable]
        public struct InputEvent {
            public UnityEvent Started;
            public UnityEvent Canceled;
            public UnityEvent Performed;

            public InputEvent(UnityEvent started, UnityEvent canceled, UnityEvent performed) {
                Started = started ?? new UnityEvent();
                Canceled = canceled ?? new UnityEvent();
                Performed = performed ?? new UnityEvent();
            }
        }
    }
    
}
