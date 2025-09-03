using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeLimit = 15f;
    private float timeRemaining;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI distanceText;
    public PogoController pogo;

    private bool gameActive = true;

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        if (!gameActive) return;

        // Contador
        timeRemaining -= Time.deltaTime;
        timeText.text = "Tiempo: " + Mathf.Ceil(timeRemaining);

        // Mostrar distancia
        distanceText.text = "Distancia: " + Mathf.Round(pogo.GetDistance());

        // Fin del juego
        if (timeRemaining <= 0f)
        {
            gameActive = false;
            Debug.Log("Tiempo terminado! Distancia final: " + pogo.GetDistance());
        }
    }
}
