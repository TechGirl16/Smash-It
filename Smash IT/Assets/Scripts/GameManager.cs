using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance for easy access

    [Header("Score UI")]
    public TMP_Text player1ScoreText; // TextMeshPro UI Text for Player 1 score
    public TMP_Text player2ScoreText; // TextMeshPro UI Text for Player 2 score
    public GameObject winPanel; // Panel to show when a player wins
    public TextMeshProUGUI winText; // TextMeshPro UI Text for win message 
    

    [Header("Gameplay")]
    public int winningScore = 5; // Score needed to win the game
    private int player1Score = 0; // Score for Player 1
    private int player2Score = 0; // Score for Player 2

    [Header("Ball Settings")]
    public GameObject ballPrefab; // Prefab for the ball that will be instantiated
    public Transform paddle1;  // Reference to the paddle for Player 1 (bottom player)
    public Transform paddle2; // Reference to the paddle for Player 2 (top player)
    private Vector2 offsetP1 = new Vector2(-1.72f, 2.12f); // Offset for paddle 1 (bottom player)
    private Vector2 offsetP2 = new Vector2(0f, -2.28f); // Offset for paddle 2 (top player)

    private string player1Label = "Player 1: "; // Label for Player 1 score
    private string player2Label = "Player 2: "; // Label for Player 2 score

    private int currentServer = 1; //   1 = paddle1 serves, 2 = paddle2 serves
    private GameObject currentBall; // Reference to the currently active ball



  
    void Awake()
    {
        if (Instance == null) Instance = this; // Ensure only one instance of GameManager exists
        else Destroy(gameObject); // Destroy duplicate instances
        

    }




    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false); // Hide win panel at the start
        SpawnBall(); // Spawn the initial ball
        UpdateScoreUI(); // Initialize score UI
        winPanel.SetActive(false); // Hide the win panel at the start
    }




    // Called by BallMovement when ball enters a goal
    public void PlayerScores(int playerNumber)
    {
        if (playerNumber == 1) player1Score++; // Increment Player 1's score
        else player2Score++; // Increment Player 2's score

        UpdateScoreUI();

        if (player1Score >= winningScore || player2Score >= winningScore) // Check if either player has reached the winning score
        {
            EndGame();
        }
        else
        {
            SwitchServer();
            SpawnBall();
        }
    }
    



    // Update the score UI text for both players
    void UpdateScoreUI()
    {
        player1ScoreText.text = player1Label + player1Score; // Update Player 1 score text
        player2ScoreText.text = player2Label + player2Score; // Update Player 2 score text
    }


    // Restart the game by reloading the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    // Quit the game application
    public void QuitGame()
    {
        Application.Quit();
    }

    // End the game and display the win panel

    void EndGame()
    {
        winPanel.SetActive(true); // Show the win panel
       // winText.text = "Player 1 Wins!";

        winText.text = (player1Score > player2Score) ? "Player 1 Wins!" : "Player 2 Wins!"; // Set the win message based on scores

        if (currentBall != null) // If a ball is currently active, destroy it
            Destroy(currentBall); //    Destroy the current ball
    }



    // Switch the server to the other player
    void SwitchServer()
    {
        currentServer = (currentServer == 1) ? 2 : 1; // Switch the server to the other player
    }




    // Spawn a new ball at the appropriate position based on the current server
    void SpawnBall()
    {
        if (currentBall != null) Destroy(currentBall); // Destroy the current ball if it exists

        Vector3 spawnPos = (currentServer == 1) // Determine spawn position based on current server
            ? paddle1.position + (Vector3)offsetP1 // Position ball at paddle 1
            : paddle2.position + (Vector3)offsetP2; // Position ball at paddle 2

        currentBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity); // Instantiate a new ball at the determined position
    }
}
