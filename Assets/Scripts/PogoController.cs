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

    void ApplyJump()
    {
        // Aplica toda la distancia acumulada en un solo movimiento
        Debug.Log("Distancia total acumulada: " + accumulatedDistance);
        transform.position += Vector3.forward * accumulatedDistance;

        // Guardar para el GameManager
        lastDistance = accumulatedDistance;

        // Resetear
        accumulatedDistance = 0f;
        timer = 0f;
        roundActive = true;
    }

    public float GetDistance()
    {
        return accumulatedDistance;
    }
}
