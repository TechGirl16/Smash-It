using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float sizeMultiplier = 1.6f;  // how much bigger the paddle gets
    public float duration = 5f;          // how long it lasts

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle") || other.CompareTag("Paddle2"))
        {
            // Grow the paddle
            PaddlePowerups paddlePower = other.GetComponent<PaddlePowerups>();
            if (paddlePower != null)
            {
                paddlePower.ApplyBig(sizeMultiplier, duration);
            }

            // Destroy the power-up after use
            Destroy(gameObject);
        }
    }
}

