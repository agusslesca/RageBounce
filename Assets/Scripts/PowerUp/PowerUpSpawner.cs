using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public Transform playerTransform;
    public float spawnInterval = 50f; // Intervalo de spawn en metros
    public float spawnHeight = 1f; // Altura de los power-ups sobre el suelo
    public PowerUpPool[] powerUpPools;

    private float nextSpawnDistance;

    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    public void StartSpawning()
    {
        StopAllCoroutines();
        // Inicializa la distancia de spawn al inicio del vuelo
        nextSpawnDistance = playerTransform.position.z + spawnInterval;
        StartCoroutine(SpawnPowerUpCoroutine());
    }

    private IEnumerator SpawnPowerUpCoroutine()
    {
        // Espera a que la posición del jugador sea válida
        while (playerTransform == null)
        {
            yield return null;
        }

        while (true)
        {
            // Espera hasta que el jugador haya avanzado lo suficiente
            while (playerTransform.position.z < nextSpawnDistance)
            {
                yield return null;
            }

            // Selecciona un pool de power-ups de forma aleatoria del array.
            if (powerUpPools.Length > 0)
            {
                PowerUpPool selectedPool = powerUpPools[Random.Range(0, powerUpPools.Length)];
                GameObject powerUpGO = selectedPool.GetPowerUp();

                if (powerUpGO != null)
                {
                    // Posiciona el Power-Up a lo largo del eje Z, con una altura y en la misma línea que el Pogo
                    Vector3 spawnPosition = new Vector3(
                        playerTransform.position.x, // Misma posición X que el jugador
                        spawnHeight, // Altura fija sobre el suelo
                        nextSpawnDistance // Posición Z basada en la distancia
                    );

                    powerUpGO.transform.position = spawnPosition;
                    powerUpGO.transform.rotation = Quaternion.identity;

                    PowerUpBehaviour powerUpBehaviour = powerUpGO.GetComponent<PowerUpBehaviour>();
                    if (powerUpBehaviour != null)
                    {
                        powerUpBehaviour.SetPool(selectedPool);
                    }
                }
            }

            // Aumenta la distancia para la próxima generación
            nextSpawnDistance += spawnInterval;
        }
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }
}