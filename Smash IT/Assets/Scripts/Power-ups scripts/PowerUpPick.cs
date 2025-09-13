using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public enum PowerUpType
    {
        BigPaddle,
        SpeedBoost,
        SlowBall,
        BigBall,
    }

    public PowerUpType type;
    public float factor = 1.5f;   // default effect strength
    public float duration = 5f;   // default duration in seconds

    private void OnTriggerEnter2D(Collider2D other)
    {
        PowerUpEffect effects = other.GetComponent<PowerUpEffect>();
        if (effects == null) return;

        switch (type)
        {
            // for the big paddle
            case PowerUpType.BigPaddle:
                effects.ApplyBigPaddle(factor, duration);
                break;
            
                // for the speed boast
            case PowerUpType.SpeedBoost:
                effects.ApplySpeedBoost(factor, duration);
                break;

                // for the slow ball
            case PowerUpType.SlowBall:
                effects.ApplySlowBall(factor, duration);
                break;
                // for big ball power up 
            case PowerUpType.BigBall:
                effects.ApplyBigBall(factor, duration); 
                break;
        }

        Destroy(gameObject); // remove power-up after use
    }
}

