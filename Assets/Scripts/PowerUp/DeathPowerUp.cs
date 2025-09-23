using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Death")]
public class DeathPowerUp : PowerUpData
{
    public override void OnCollect(GameManager gm, PogoController pogo)
    {
        if (gm != null)
            gm.LoseLife();
    }
}

