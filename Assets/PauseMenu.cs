using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private bool isPaused = false;

    // Devuelve si el juego está pausado
    public bool IsPaused()
    {
        return isPaused;
    }

    // Toggle del menú de pausa
    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        UpdateTimeScale();
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        UpdateTimeScale();
    }

    private void UpdateTimeScale()
    {
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
