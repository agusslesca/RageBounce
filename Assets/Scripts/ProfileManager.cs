using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [Header("Perfil Actual")]
    public Image currentProfilePic;

    [Header("Panel de Selección")]
    public GameObject selectionPanel;
    public Sprite[] availablePics;

    public PauseMenu pauseMenu; // referencia al PauseMenu

    private int currentIndex = 0;

    void Start()
    {
        if (selectionPanel != null)
            selectionPanel.SetActive(false);

        if (availablePics.Length > 0)
            currentProfilePic.sprite = availablePics[0];
    }

    // Abrir panel de perfil
    public void OpenPanel()
    {
        selectionPanel.SetActive(true);

        // Pausar solo si el menú de pausa no está abierto
        if (pauseMenu != null && !pauseMenu.IsPaused())
            Time.timeScale = 0f;
    }

    // Cerrar panel de perfil
    public void ClosePanel()
    {
        selectionPanel.SetActive(false);

        // Reanudar solo si el menú de pausa no está abierto
        if (pauseMenu != null && !pauseMenu.IsPaused())
            Time.timeScale = 1f;
    }

    // Cambiar avatar
    public void SetProfilePic(int index)
    {
        if (index < 0 || index >= availablePics.Length) return;

        currentIndex = index;
        currentProfilePic.sprite = availablePics[index];

        PlayerPrefs.SetInt("ProfileIndex", currentIndex);
        PlayerPrefs.Save();

        // ¡NO cerrar el panel ni tocar Time.timeScale aquí!
    }
}
