using UnityEngine;

public class PaddleMovementScript : MonoBehaviour
{
    public float moveSpeed = 15f;

    private KeyCode upKey, downKey, leftKey, rightKey;

    // Screen edges
    private float leftLimit, rightLimit, bottomLimit, topLimit;

    void Start()
    {
        // Pick keys depending on which paddle this is
        if (CompareTag("Paddle"))
        {
            upKey = KeyCode.W;
            downKey = KeyCode.S;
            leftKey = KeyCode.A;
            rightKey = KeyCode.D;
        }
        else if (CompareTag("Paddle2"))
        {
            upKey = KeyCode.UpArrow;
            downKey = KeyCode.DownArrow;
            leftKey = KeyCode.LeftArrow;
            rightKey = KeyCode.RightArrow;
        }

        // Get screen edges in world space
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        leftLimit = bottomLeft.x;
        rightLimit = topRight.x;
        bottomLimit = bottomLeft.y;
        topLimit = topRight.y;
    }

    void Update()
    {
        //  Work out which direction to move
        Vector3 move = Vector3.zero;

        if (Input.GetKey(upKey)) move.y += 1;
        if (Input.GetKey(downKey)) move.y -= 1;
        if (Input.GetKey(leftKey)) move.x -= 1;
        if (Input.GetKey(rightKey)) move.x += 1;

        //  Move the paddle
        transform.Translate(move.normalized * moveSpeed * Time.deltaTime);

        // Stop paddle from leaving screen
        float x = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
        float y = Mathf.Clamp(transform.position.y, bottomLimit, topLimit);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}



