using UnityEngine;

public class Powerup2 : MonoBehaviour
{
    public float sizeMultiplier = 1.6f;  // How much bigger the ball gets
    public float duration = 5f;          // How long the size increase lasts (in seconds)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided with the power-up is tagged as "Ball"
        if (other.CompareTag("Ball"))
        {
            // Get the BallPowerups component from the ball
            BallPowerups ballPower = other.GetComponent<BallPowerups>();
            if (ballPower != null)
            {
                // Apply the size increase effect to the ball
                ballPower.ApplyBig(sizeMultiplier, duration);
            }

            // Destroy the power-up after it is collected
            Destroy(gameObject);
        }
    }
}