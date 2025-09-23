using System.Collections;
using UnityEngine;

public class PogoController : MonoBehaviour
{
    public PowerBar powerBar;
    public float baseJumpForce = 5f;

    private float accumulatedDistance = 0f;
    private float lastDistance = 0f;
    private float roundTime = 15f;
    private float timer = 0f;
    private bool roundActive = true;

    void Update()
    {
        //  No hacer nada si el juego no está activo
        if (!GameManager.instance.IsGameActive()) return;

        if (roundActive)
        {
            timer += Time.deltaTime;

            // Cuando toco SPACE
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PowerBar.JumpResult result = powerBar.GetJumpResult();
                float multiplier = powerBar.GetMultiplier(result);

                float jump = baseJumpForce * multiplier;
                accumulatedDistance += jump;

                Debug.Log($"Salto acumulado: +{jump} ({result})");

                // Si fue rojo  perder vida
                if (result == PowerBar.JumpResult.Red)
                {
                    GameManager.instance.LoseLife();
                }
            }

            // Cuando se termina el tiempo aplicar salto
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

    public void ApplyJump()
    {
        Debug.Log("Distancia total acumulada: " + accumulatedDistance);

        lastDistance = accumulatedDistance;

        StartCoroutine(FlyForward(accumulatedDistance));

        accumulatedDistance = 0f;
        timer = 0f;
        roundActive = false;
    }

    private IEnumerator FlyForward(float distance)
    {
        float duration = 2f;
        float elapsed = 0f;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.forward * distance;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float height = Mathf.Sin(t * Mathf.PI) * 5f;
            transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        roundActive = true; // volver a activar ronda
    }

    // Permite que los powerups agreguen metros al acumulado
    public void AddDistance(float meters)
    {
        accumulatedDistance += meters;
        Debug.Log($"AddDistance: +{meters}m -> acumulado = {accumulatedDistance}");
    }

}