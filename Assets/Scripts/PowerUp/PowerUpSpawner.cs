using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public Transform playerTransform;
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 20f;
    // Ahora es un array para manejar múltiples tipos de pools
    public PowerUpPool[] powerUpPools;

    void Start()
    {
        StartCoroutine(SpawnPowerUpCoroutine());
    }

    private IEnumerator SpawnPowerUpCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Espera un tiempo aleatorio

            // Selecciona un pool de power-ups de forma aleatoria del array
            PowerUpPool selectedPool = powerUpPools[Random.Range(0, powerUpPools.Length)];

            // Obtiene un Power-Up del pool seleccionado
            GameObject powerUpGO = selectedPool.GetPowerUp();

            if (powerUpGO != null)
            {
                // Posiciona y rota el Power-Up
                float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
                Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;
                spawnPosition.y = 1f; // Ajusta la altura si es necesario

                powerUpGO.transform.position = spawnPosition;
                powerUpGO.transform.rotation = Quaternion.identity;

                // Asigna el pool correcto al Power-Up para que sepa dónde regresar
                PowerUpBehaviour powerUpBehaviour = powerUpGO.GetComponent<PowerUpBehaviour>();
                if (powerUpBehaviour != null)
                {
                    powerUpBehaviour.SetPool(selectedPool);
                }
            }
        }
    }
}