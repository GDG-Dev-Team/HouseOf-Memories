using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;

    [SerializeField]
    GameObject music;

    void Awake()
    {
        if (!instance)
            instance = this;
    }

    public void LoadMenu(string menuName)
    {
        SceneManager.LoadScene(menuName, LoadSceneMode.Additive);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UnloadMenu(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void Pause()
    {
        PlayerPause.instance.PerformPause();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
