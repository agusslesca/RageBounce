using UnityEngine;
using UnityEngine.SceneManagement; // Para el Quit (si quieres volver al men� principal)

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Panel del men� de pausa
    private bool isPaused = false;

    void Update()
    {
        // Tambi�n puedes pausar con la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
            Time.timeScale = 0f; // Pausa el juego
        else
            Time.timeScale = 1f; // Reanuda el juego
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        // Si est�s en editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
