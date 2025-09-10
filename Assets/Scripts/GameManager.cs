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
    private bool gameActive = true;

    [Header("Vidas")]
    public int maxLives = 3;
    private int currentLives;
    public Image[] hearts; // arrastrar imágenes de corazones
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Vida Perdida")]
    public GameObject lifeLostPanel; // arrastrar LifeLostPanel

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
    }

    void Update()
    {
        if (!gameActive) return;

        // Contador
        timeRemaining -= Time.deltaTime;
        timeText.text = "Tiempo: " + Mathf.Ceil(timeRemaining);

        // Mostrar distancia acumulada
        distanceText.text = "Distancia: " + Mathf.Round(pogo.GetDistance());

        // Fin del juego
        if (timeRemaining <= 0f)
        {
            gameActive = false;

            // Ocultar textos
            timeText.gameObject.SetActive(false);
            tiempoImg.gameObject.SetActive(false);

            Debug.Log("Tiempo terminado! Distancia final: " + pogo.GetDistance());
        }
    }

    // Función pública para perder vida
    public void LoseLife()
    {
        currentLives--;
        UpdateHeartsUI();

        if (lifeLostPanel != null)
            StartCoroutine(LifeLostCoroutine());

        if (currentLives <= 0)
        {
            Debug.Log("Game Over!");
            // Aquí podés reiniciar la escena o mostrar menú final
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

    public void ShowLifeLostPopup()
    {
        StartCoroutine(LifeLostCoroutine());
    }

    private IEnumerator LifeLostCoroutine()
    {
       
        
            lifeLostPanel.SetActive(true);
            yield return new WaitForSeconds(1f); // tiempo que se muestra
            lifeLostPanel.SetActive(false);
        
    }
}
