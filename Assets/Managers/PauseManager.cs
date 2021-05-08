using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public bool IsPaused { get; private set; }
    public event System.Action<bool> Paused;

    private void Awake()
    {
        Instance = this;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        IsPaused = true;
        Paused?.Invoke(IsPaused);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        IsPaused = false;
        Paused?.Invoke(IsPaused);
    }
}
