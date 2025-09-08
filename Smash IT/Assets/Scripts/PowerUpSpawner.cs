using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;     // assign your prefab here
    public float firstDelay = 3f;        // wait before first spawn
    public float spawnInterval = 10f;    // seconds between spawns
    public BoxCollider2D[] spawnAreas;   // [0] = Player1 side, [1] = Player2 side

    private int nextSpawnIndex = 0;      // alternates between 0 and 1

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            // Only spawn if no power-up is already active
            if (GameObject.FindWithTag("Power up") == null)
            {
                // Pick the current spawn area (alternates each time)
                BoxCollider2D chosenArea = spawnAreas[nextSpawnIndex];
                Vector3 pos = GetRandomPointInBounds(chosenArea.bounds);

                Instantiate(powerUpPrefab, pos, Quaternion.identity);

                // Switch to the other side for next spawn
                nextSpawnIndex = 1 - nextSpawnIndex;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0f);
    }
}
