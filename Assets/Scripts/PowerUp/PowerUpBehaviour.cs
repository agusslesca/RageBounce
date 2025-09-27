using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public PowerUpData powerUpData;
    private PowerUpPool myPool;

    // Llama a este m�todo para inicializar el Power-Up
    public void SetPool(PowerUpPool pool)
    {
        myPool = pool;
    }

    // Este m�todo se llama cuando el jugador lo recolecta
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gm = FindAnyObjectByType<GameManager>();
            PogoController pogo = other.GetComponent<PogoController>();

            if (powerUpData != null)
            {
                powerUpData.OnCollect(gm, pogo);
            }

            // Devolvemos el Power-Up al pool en lugar de destruirlo
            myPool.ReturnPowerUp(this.gameObject);
        }
    }
}


