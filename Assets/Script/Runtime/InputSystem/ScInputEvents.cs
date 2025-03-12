using UnityEngine;


namespace TD.Runtime.InputSystem {
    public class ScInputEvents : MonoBehaviour {
        ScInputManager _inputManager => ScInputManager.Instance;
        void Start() {
            SetUpEvents();
        }
        
        void SetUpEvents() {
            _inputManager.OnMoveEvent.Performed += OnMoveStart;
            _inputManager.OnMoveEvent.Canceled += OnMoveCancel;
            
            _inputManager.OnViewEvent.Performed += OnViewStart;
            _inputManager.OnViewEvent.Canceled += OnViewCancel;
            
            _inputManager.OnInteractEvent.Performed += OnInteractStart;
            _inputManager.OnInteractEvent.Canceled += OnInteractStop;
            
            _inputManager.OnEscapeEvent.Performed += OnEscapeStart;
            _inputManager.OnEscapeEvent.Canceled += OnEscapeStop;
        }
        
        
        void OnMoveStart() => _inputManager.IsMoving = true;
        void OnMoveCancel() => _inputManager.IsMoving = false;

        void OnViewStart() => _inputManager.IsViewing = true;
        void OnViewCancel() => _inputManager.IsViewing = false;
        
        void OnInteractStart() => _inputManager.IsInteracting = true;
        void OnInteractStop() => _inputManager.IsInteracting = false;
        
        void OnEscapeStart() => _inputManager.IsEscaping = true;
        void OnEscapeStop() => _inputManager.IsEscaping = false;
    }
}
