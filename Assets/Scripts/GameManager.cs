using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Tiempo")]
    public float timeLimit = 15f;
    private float timeRemaining;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI distanceText;
    public Image tiempoImg;

    [Header("Pogo")]
    public PogoController pogo;
    private bool gameActive = false;

    [Header("Vidas")]
    public int maxLives = 3;
    private int currentLives;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Vida Perdida")]
    public GameObject lifeLostPanel;

    [Header("Game Over")]
    public GameObject gameOverPanel; //  arrastrar panel del Canvas

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = timeLimit;
        currentLives = maxLives;
        UpdateHeartsUI();

        // Al inicio, aseguramos que el panel esté oculto
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!gameActive) return;

        // Contador
        timeRemaining -= Time.deltaTime;
        timeText.text = "Tiempo: " + Mathf.Ceil(timeRemaining);

        distanceText.text = "Distancia: " + Mathf.Round(pogo.GetDistance());

        // Fin del juego
        if (timeRemaining <= 0f)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        gameActive = true;
    }

    public bool IsGameActive()
    {
        return gameActive;
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateHeartsUI();

        if (lifeLostPanel != null)
            StartCoroutine(LifeLostCoroutine());

        if (currentLives <= 0)
        {
            EndGame();
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

    private IEnumerator LifeLostCoroutine()
    {
        lifeLostPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        lifeLostPanel.SetActive(false);
    }

    //  función para terminar el juego
    private void EndGame()
    {
        gameActive = false;

        timeText.gameObject.SetActive(false);
        tiempoImg.gameObject.SetActive(false);

        Debug.Log("Game Over!");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    //  Botones del menú
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MenuPrincipal"); //  asegurate de tener esta escena en Build Settings
    }
}
