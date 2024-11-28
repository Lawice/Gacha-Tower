using TD.InputSystem;
using UnityEngine;

namespace TD.Player {
    public class ScFlyCamera : MonoBehaviour {
        ScInputManager _inputManager => ScInputManager.Instance;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _viewSensibility;
        float yRota = 0;
        float xRota = 0;
        
        private void Start() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        private void Update() {

            if (_inputManager.IsMoving && !_inputManager.IsCameraLocked) {
                Move();
            }

            if (_inputManager.IsViewing && !_inputManager.IsCameraLocked) {
                View();
            }
        }

        void Move() {
            transform.position += transform.forward * (_inputManager.MoveValue.z * (_speed * Time.deltaTime));
            transform.position += transform.right * (_inputManager.MoveValue.x * (_speed * Time.deltaTime));
            transform.position += transform.up * (_inputManager.MoveValue.y * (_speed * Time.deltaTime));
        }

        void View() {
            yRota += _inputManager.ViewValue.x * Time.deltaTime * _viewSensibility;
            xRota -= _inputManager.ViewValue.y * Time.deltaTime * _viewSensibility;
            xRota = Mathf.Clamp(xRota, -90, 90);
            
            transform.rotation = Quaternion.Euler(new Vector3(xRota, yRota, 0));
        }
    }
}
