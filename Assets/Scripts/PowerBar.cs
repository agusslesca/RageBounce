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

    // Enum para resultados
    public enum JumpResult { Green, Yellow, Red }

    void Update()
    {
        if (movingRight)
            slider.value += Time.deltaTime * speed;
        else
            slider.value -= Time.deltaTime * speed;

        if (slider.value >= 1f) movingRight = false;
        if (slider.value <= 0f) movingRight = true;
    }

    // Devuelve el resultado del salto
    public JumpResult GetJumpResult()
    {
        float val = slider.value;

        if (val >= greenMin && val <= greenMax)
            return JumpResult.Green;
        else if (val >= yellowMin && val <= yellowMax)
            return JumpResult.Yellow;
        else
            return JumpResult.Red;
    }

    // Devuelve el multiplicador según resultado
    public float GetMultiplier(JumpResult result)
    {
        switch (result)
        {
            case JumpResult.Green: return 2f;
            case JumpResult.Yellow: return 1.2f;
            case JumpResult.Red: return 0.5f;
        }
        return 1f;
    }
}
