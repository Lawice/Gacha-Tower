using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMainMenu : MonoBehaviour {

    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}