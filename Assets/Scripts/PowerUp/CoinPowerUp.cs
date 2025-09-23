using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Coin")]
public class CoinPowerUp : PowerUpData
{
    public int value = 10;

    public override void OnCollect(GameManager gm, PogoController pogo)
    {
        if (gm != null)
            gm.AddScore(value);
    }
}

