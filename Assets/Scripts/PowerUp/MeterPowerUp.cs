using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Meter")]
public class MeterPowerUp : PowerUpData
{
    public float bonusMeters = 5f;

    public override void OnCollect(GameManager gm, PogoController pogo)
    {
        // Preferible que el Pogo maneje su propia distancia
        if (pogo != null)
            pogo.AddDistance(bonusMeters);

        // opcional: también mostrar feedback en GM (ej. sumar a un contador)
        // if (gm != null) gm.ShowPickupFeedback("+ " + bonusMeters + "m");
    }
}
