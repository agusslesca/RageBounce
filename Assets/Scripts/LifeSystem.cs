using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LifeSystem : MonoBehaviour
{
    [Header("Configuración de vidas")]
    public int maxLives = 3;
    private int currentLives;

    [Header("UI de corazones")]
    public Image[] hearts; // arrastrá las imágenes de corazones en el Canvas
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Mensaje de vida perdida")]
    public GameObject lostLifePanel;
    public TextMeshProUGUI lostLifeText;

    void Start()
    {
        currentLives = maxLives;

        if (lostLifePanel != null)
            lostLifePanel.SetActive(false);

        UpdateHeartsUI();
    }

    public void LoseLife()
    {
        if (currentLives <= 0) return;

        currentLives--;
        UpdateHeartsUI();

        if (lostLifePanel != null)
            StartCoroutine(ShowLostLifeMessage());

        if (currentLives <= 0)
        {
            Debug.Log("Game Over!");
            // Acá podés poner un Game Over panel, reiniciar escena, etc.
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    private IEnumerator ShowLostLifeMessage()
    {
        lostLifePanel.SetActive(true);
        lostLifeText.text = "¡Has perdido una vida!";
        yield return new WaitForSecondsRealtime(2f);
        lostLifePanel.SetActive(false);
    }
}
