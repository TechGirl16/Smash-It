using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BallMovement : MonoBehaviour
{
    private float launchForce = 10f;
    private Rigidbody2D rb;
    private Collider2D col;
    public bool isLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.bodyType = RigidbodyType2D.Kinematic; // ball won't move until serve
        col.isTrigger = true; // paddle passes through for first serve
    }

    public void LaunchFromPaddle(Transform paddleTransform)
    {
        if (isLaunched) return;

        isLaunched = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.isTrigger = false;

        //  Calculate offset between ball and paddle center
        float xOffset = transform.position.x - paddleTransform.position.x;

        // Base direction is up if paddle1, down if paddle2
        Vector2 baseDir = paddleTransform.CompareTag("Paddle") ? Vector2.up : Vector2.down;

        // Add a little  (spin) based on where ball hit paddle
        Vector2 dir = (baseDir + new Vector2(xOffset, 0)).normalized;

        rb.linearVelocity = dir * launchForce;
    }

    // For human-controlled paddles
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isLaunched) return;

        if (other.CompareTag("Paddle") || other.CompareTag("Paddle2"))
        {
            LaunchFromPaddle(other.transform); // now uses the public method
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Goal"))
        {
            GameManager.Instance.PlayerScores(2); // Player 2 scores
        }
        else if (collision.collider.CompareTag("Goalp2"))
        {
            GameManager.Instance.PlayerScores(1); // Player 1 scores
        }
    }
}






