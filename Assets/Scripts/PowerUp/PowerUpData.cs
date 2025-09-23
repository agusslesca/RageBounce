using UnityEngine;

// Clase base (NO hereda de MonoBehaviour)
public abstract class PowerUpData : ScriptableObject
{
    public string powerUpName;
    public Sprite icon; // opcional para UI

    // Se ejecuta cuando el jugador recoge el powerup.
    // Recibe referencias a GameManager y al Pogo (si necesita manipular distancia).
    public abstract void OnCollect(GameManager gm, PogoController pogo);
}
