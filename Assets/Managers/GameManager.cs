using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseManager.Instance.IsPaused)
            {
                PauseManager.Instance.Unpause();
            }
            else
            {
                PauseManager.Instance.Pause();
            }
        }
    }
}
