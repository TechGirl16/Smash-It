using UnityEngine;

public class AIPaddleMovement : MonoBehaviour
{
    [Header("Ball (assign or tag as 'Ball')")]
    public Transform ball;
    public Rigidbody2D ballRb;
    public BallMovement ballMovement;

    [Header("AI Settings")]
    public float moveSpeed = 6f;
    public float serveDistance = 1.5f;  // how close paddle must be to serve
    public float smoothness = 3f;

    [Header("Movement Bounds")]
    public float leftLimit = -7f, rightLimit = 7f;
    public float bottomLimit = -4f, topLimit = 4f;

    void Update()
    {
        // Ensure we have the ball reference
        if (ball == null || ballRb == null)
        {
            GameObject b = GameObject.FindWithTag("Ball");
            if (b == null) return;
            ball = b.transform;
            ballRb = b.GetComponent<Rigidbody2D>();
            if (ballMovement == null) ballMovement = b.GetComponent<BallMovement>();
        }

        //  If ball is NOT launched → move paddle toward it until close enough, then serve
        if (ballMovement != null && !ballMovement.isLaunched)
        {
            // move toward the ball’s x-position
            Vector3 targetPos = new Vector3(ball.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // when close enough, launch
            if (Vector2.Distance(ball.position, transform.position) < serveDistance)
            {
                ballMovement.LaunchFromPaddle(transform);
            }

            return; // stop here until ball is launched
        }

        //  Normal defense → follow ball only when coming toward AI
        Vector2 v = ballRb.linearVelocity;
        if (v.sqrMagnitude < 0.01f) return;

        Vector2 toPaddle = (Vector2)transform.position - (Vector2)ball.position;
        bool ballComingTowardMe = Vector2.Dot(v, toPaddle) > 0f;
        if (!ballComingTowardMe) return;

        // move toward ball’s x smoothly
        Vector3 followPos = new Vector3(ball.position.x, transform.position.y, transform.position.z); 
        transform.position = Vector3.Lerp(transform.position, followPos, smoothness * Time.deltaTime);

        // clamp bounds
        float x = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
        float y = Mathf.Clamp(transform.position.y, bottomLimit, topLimit);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}



