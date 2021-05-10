using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; }
    public static event System.Action<bool> Paused;

    public static void Pause()
    {
        Time.timeScale = 0;
        IsPaused = true;
        Paused?.Invoke(IsPaused);
    }

    public static void Unpause()
    {
        Time.timeScale = 1;
        IsPaused = false;
        Paused?.Invoke(IsPaused);
    }
}
