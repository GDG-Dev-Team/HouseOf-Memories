using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    [SerializeField]
    GameObject musicSource;

    public void MuteToggle(bool muted)
    {
        AudioListener.volume = muted ? 0 : 1;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void musicToggle(bool muteMusic)
    {
        musicSource.GetComponent<AudioSource>().volume = muteMusic ? 0 : 1;
    }
}
