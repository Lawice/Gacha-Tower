using System;
using TD.Runtime.InputSystem;
using UnityEngine;

public class ScGachaSwitcher : MonoBehaviour {
    [SerializeField] Camera _gameCamera;
    [SerializeField] Camera _gachaCamera;
    [SerializeField] private GameObject _gachaSystem;
    [SerializeField] private GameObject _canvas;
    
    ScInputManager _inputManager => ScInputManager.Instance;
    private void Start() {
        GachaClose();
        _inputManager.OnGachaOpenEvent.Performed.AddListener(GachaOpen);
    }
    
    private void GachaOpen() {
       _gachaSystem.SetActive(true);
       _gameCamera.gameObject.SetActive(false);
       _gachaCamera.enabled = true;
       _canvas.SetActive(false);
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
    }
    
    void GachaClose() {
        _gachaSystem.SetActive(false);
        _gameCamera.gameObject.SetActive(true);
        _gachaCamera.enabled = false;
        _canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
