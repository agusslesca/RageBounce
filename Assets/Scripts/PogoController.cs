using System.Collections;
using UnityEngine;

public class PogoController : MonoBehaviour
{
    public PowerBar powerBar;
    public float baseJumpForce = 5f;
    public PowerUpSpawner powerUpSpawner;

    private float accumulatedDistance = 0f;
    private float lastDistance = 0f;
    private float roundTime = 15f;
    private float timer = 0f;
    private bool roundActive = true;

    void Update()
    {
        if (!GameManager.instance.IsGameActive()) return;

        if (roundActive)
        {
            timer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PowerBar.JumpResult result = powerBar.GetJumpResult();
                float multiplier = powerBar.GetMultiplier(result);

                float jump = baseJumpForce * multiplier;
                accumulatedDistance += jump;

                Debug.Log($"Salto acumulado: +{jump} ({result})");

                if (result == PowerBar.JumpResult.Red)
                {
                    GameManager.instance.LoseLife();
                }
            }

            if (timer >= roundTime)
            {
                roundActive = false;
                ApplyJump();
            }
        }
    }

    // Asegúrate de que este método exista y sea público
    public float GetDistance()
    {
        return accumulatedDistance;
    }

    public void ApplyJump()
    {
        Debug.Log("Distancia total acumulada: " + accumulatedDistance);

        lastDistance = accumulatedDistance;

        if (accumulatedDistance > 0)
        {
            if (powerUpSpawner != null)
            {
                powerUpSpawner.SetPlayerTransform(transform);
            }
            StartCoroutine(FlyForward(accumulatedDistance));
        }
        else
        {
            roundActive = true;
            accumulatedDistance = 0f;
            timer = 0f;
        }
    }

    private IEnumerator FlyForward(float distance)
    {
        if (powerUpSpawner != null)
        {
            powerUpSpawner.StartSpawning();
        }

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

        if (powerUpSpawner != null)
        {
            powerUpSpawner.StopSpawning();
        }

        accumulatedDistance = 0f;
        timer = 0f;
        roundActive = true;
    }

    public void AddDistance(float meters)
    {
        accumulatedDistance += meters;
        Debug.Log($"AddDistance: +{meters}m -> acumulado = {accumulatedDistance}");
    }
}