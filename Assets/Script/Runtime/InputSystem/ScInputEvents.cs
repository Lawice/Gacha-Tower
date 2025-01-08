using UnityEngine;


namespace TD.Runtime.InputSystem {
    public class ScInputEvents : MonoBehaviour {
        ScInputManager _inputManager => ScInputManager.Instance;
        void Start() {
            SetUpEvents();
        }
        
        void SetUpEvents() {
            _inputManager.OnMoveEvent.Performed.AddListener(OnMoveStart);
            _inputManager.OnMoveEvent.Canceled.AddListener(OnMoveCancel);
            
            _inputManager.OnViewEvent.Performed.AddListener(OnViewStart);
            _inputManager.OnViewEvent.Canceled.AddListener(OnViewCancel);
            
            _inputManager.OnInteractEvent.Performed.AddListener(OnInteractStart);
            _inputManager.OnInteractEvent.Canceled.AddListener(OnInteractStop);
            
            _inputManager.OnEscapeEvent.Performed.AddListener(OnEscapeStart);
            _inputManager.OnEscapeEvent.Canceled.AddListener(OnEscapeStop);
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
