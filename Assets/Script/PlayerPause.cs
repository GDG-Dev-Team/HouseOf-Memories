using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerPause : MonoBehaviour
{
    public static PlayerPause instance;

    public static bool isPaused = false;

    void Awake()
    {
        if (!instance)
            instance = this;
        isPaused = false;
    }

    public void Escape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PerformPause();
        }
    }

    public void PerformPause()
    {
        if (!isPaused)
        {
            SceneManage.instance.LoadMenu("Pause Menu");
        }
        else
        {
            SceneManage.instance.UnloadMenu("Pause menu");
        }
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        // GetComponent<PlayerShoot>().canShoot = !isPaused;
    }
}
