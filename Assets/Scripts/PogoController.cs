using System.Collections;
using UnityEngine;

public class PogoController : MonoBehaviour
{
    public PowerBar powerBar;
    public float baseJumpForce = 5f;

    private float accumulatedDistance = 0f;  // todos los saltos de la ronda
    private float lastDistance = 0f;         // cu�nto se movi� en la �ltima ronda
    private float roundTime = 15f;
    private float timer = 0f;
    private bool roundActive = true;

    void Update()
    {
        if (roundActive)
        {
            timer += Time.deltaTime;

            // Cada vez que toca espacio, acumula fuerza
            if (Input.GetKeyDown(KeyCode.Space))
            {
                float multiplier = powerBar.GetMultiplier();
                float jump = baseJumpForce * multiplier;
                accumulatedDistance += jump;
                Debug.Log("Salto acumulado: +" + jump);
            }

            // Si pas� el tiempo  aplicar distancia
            if (timer >= roundTime)
            {
                roundActive = false;
                ApplyJump();
            }
        }
    }

    public float GetDistance()
    {
        return accumulatedDistance;
    }

    void ApplyJump()
    {
        // Aplica toda la distancia acumulada en un solo movimiento
        Debug.Log("Distancia total acumulada: " + accumulatedDistance);
        

        // Guardar para el GameManager
        lastDistance = accumulatedDistance;

        // Lanzar corutina de vuelo
        StartCoroutine(FlyForward(accumulatedDistance));

        // Resetear
        accumulatedDistance = 0f;
        timer = 0f;
        roundActive = false;
    }

    private IEnumerator FlyForward(float distance)
    {
        float duration = 2f; // duraci�n del vuelo en segundos
        float elapsed = 0f;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.forward * distance;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // trayectoria tipo arco parab�lico (para que �vuele�)
            float height = Mathf.Sin(t * Mathf.PI) * 5f; // altura m�xima = 5

            transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos; // asegurar que llegue al destino
        roundActive = true; // volver a activar ronda despu�s de volar
    }
}
