using UnityEngine;

public class PaddleMovementScript : MonoBehaviour
{
    public float moveSpeed = 15f;

    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode leftKey;
    private KeyCode rightKey;

    [Header("Movement Limits")]
    private float leftLimit = -5f; 
    private float  rightLimit = 5f; 
    private float bottomLimit = -7f;
    private float topLimit = -2f;

    void Start()
    {
        // Assign keys based on tag
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
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(upKey)) moveDirection += Vector3.up;
        if (Input.GetKey(downKey)) moveDirection += Vector3.down;
        if (Input.GetKey(leftKey)) moveDirection += Vector3.left;
        if (Input.GetKey(rightKey)) moveDirection += Vector3.right;

        if (moveDirection != Vector3.zero)
        {
            // Move only if input is pressed
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }

        // Clamp position
        float x = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
        float y = Mathf.Clamp(transform.position.y, bottomLimit, topLimit);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}


