using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton

    [Header("Tiempo")]
    public float timeLimit = 15f;
    private float timeRemaining;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI distanceText;
    public Image tiempoImg;

    [Header("Pogo")]
    public PogoController pogo;
    private bool gameActive = false; // empieza en falso hasta que se toque Start

    [Header("Vidas")]
    public int maxLives = 3;
    private int currentLives;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Vida Perdida")]
    public GameObject lifeLostPanel;

    [Header("UI Inicio / Derrota")]
    public GameObject startButton;       // botón Start
    public GameObject defeatMenu;        // panel de derrota
    public string mainMenuScene = "MainMenu"; // nombre de tu menú principal

    [Header("Puntaje")]
    public int score = 0;
    public TextMeshProUGUI scoreText; // arrastrar en inspector (opcional)

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentLives = maxLives;
        UpdateHeartsUI();

        // al iniciar, tiempo y paneles apagados
        timeRemaining = timeLimit;
        if (timeText != null) timeText.gameObject.SetActive(false);
        if (tiempoImg != null) tiempoImg.gameObject.SetActive(false);
        if (defeatMenu != null) defeatMenu.SetActive(false);
    }

    void Update()
    {
        if (!gameActive) return;

        // Contador
        timeRemaining -= Time.deltaTime;
        timeText.text = "Tiempo: " + Mathf.Ceil(timeRemaining);

        // Mostrar distancia acumulada
        distanceText.text = "Distancia: " + Mathf.Round(pogo.GetDistance());

        // Fin del tiempo  si todavía hay vidas, que el pogo salte
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            gameActive = false; // pausa mientras salta
            pogo.ApplyJump();   //  aquí se lanza el salto acumulado
        }
    }

    // --- BOTÓN START ---
    public void StartGame()
    {
        gameActive = true;
        timeRemaining = timeLimit; // reinicia el contador SOLO al empezar

        if (timeText != null) timeText.gameObject.SetActive(true);
        if (tiempoImg != null) tiempoImg.gameObject.SetActive(true);
        if (startButton != null) startButton.SetActive(false); // ocultar botón
    }

    public bool IsGameActive()
    {
        return gameActive;
    }

    public void SetGameActive(bool value)
    {
        gameActive = value;
    }

    // --- VIDAS ---
    public void LoseLife()
    {
        currentLives--;
        UpdateHeartsUI();

        if (lifeLostPanel != null)
            StartCoroutine(LifeLostCoroutine());

        if (currentLives <= 0)
        {
            ShowDefeatMenu();
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

    // --- DERROTA ---
    private void ShowDefeatMenu()
    {
        gameActive = false;

        if (timeText != null) timeText.gameObject.SetActive(false);
        if (tiempoImg != null) tiempoImg.gameObject.SetActive(false);

        if (defeatMenu != null)
            defeatMenu.SetActive(true); // mostrar menú de derrota
    }

    // --- BOTONES DEL MENÚ DERROTA ---
    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuScene);
    }

    // --- POWER UP ---

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null) scoreText.text = score.ToString();
    }
}


