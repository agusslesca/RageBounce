using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public int poolSize = 10;
    private Queue<GameObject> poolQueue;

    void Start()
    {
        poolQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newPowerUp = Instantiate(powerUpPrefab);
            newPowerUp.SetActive(false); // Lo desactivamos para que no sea visible
            poolQueue.Enqueue(newPowerUp);
        }
    }

    public GameObject GetPowerUp()
    {
        if (poolQueue.Count > 0)
        {
            GameObject powerUp = poolQueue.Dequeue();
            powerUp.SetActive(true);
            return powerUp;
        }
        else
        {
            // Opcional: crea uno nuevo si el pool está vacío
            Debug.LogWarning("Pool está vacío. Creando nuevo objeto.");
            return Instantiate(powerUpPrefab);
        }
    }

    public void ReturnPowerUp(GameObject powerUp)
    {
        powerUp.SetActive(false);
        poolQueue.Enqueue(powerUp);
    }
}