using UnityEngine;
using System.Collections;

public class PowerUpSpawner2 : MonoBehaviour
{
    public GameObject powerUp2; 
    public float firstWait = 10f; // wait time
    public float waitBetween = 17f; // time between spawn
    public BoxCollider2D[] spawnPlaces; // Areas where power-ups can appear (0 = Player 1 side, 1 = Player 2 side)
    private int whichSide = 0; // Which side to spawn on next (0 or 1)

    void Awake()
    {
        StartCoroutine(SpawnPowerUps());
    }
    private IEnumerator SpawnPowerUps() // Loop to keep spawning power-ups
    {
        yield return new WaitForSeconds(firstWait);  // Wait before spawning the first power-up
        
        while (true)
        {
            if (GameObject.FindWithTag("Power up") == null) // Check if there's no power-up already in the game
            {
                BoxCollider2D area = spawnPlaces[whichSide];  // Pick the spawn area (Player 1 or Player 2)
                Vector3 spot = GetRandomSpot(area.bounds);
                
                Instantiate(powerUp2, spot, Quaternion.identity); // Create the Powerup2 at that spot
                
                if (whichSide == 0)  // Switch to the other side for next spawn
                    whichSide = 1;
                else
                    whichSide = 0;
            }
            
            yield return new WaitForSeconds(waitBetween); // Wait before trying to spawn again
        }
    }
    private Vector3 GetRandomSpot(Bounds area)
    {
        float x = Random.Range(area.min.x, area.max.x);
        float y = Random.Range(area.min.y, area.max.y);
        return new Vector3(x, y, 0f);
    }
}