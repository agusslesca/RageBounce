using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider slider;
    public float speed = 2f; // velocidad de movimiento de la barra
    private bool movingRight = true;

    // Rangos de zonas (0 a 1 en el slider)
    private float greenMin = 0.45f;
    private float greenMax = 0.55f;

    private float yellowMin = 0.30f;
    private float yellowMax = 0.70f;

    // Se va actualizando siempre
    void Update()
    {
        if (movingRight)
            slider.value += Time.deltaTime * speed;
        else
            slider.value -= Time.deltaTime * speed;

        if (slider.value >= 1f) movingRight = false;
        if (slider.value <= 0f) movingRight = true;
    }

    // Llamar cuando el jugador aprieta SPACE
    public float GetMultiplier()
    {
        float val = slider.value;

        if (val >= greenMin && val <= greenMax)
        {
            return 2f; // VERDE
        }
        else if (val >= yellowMin && val <= yellowMax)
        {
            return 1.2f; // AMARILLO
        }
        else
        {
            return 0.5f; // ROJO
        }
    }
}

