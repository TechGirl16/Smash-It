using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Power-Up Prefabs (drag them here)")]
    public GameObject[] powerUpPrefabs;

    [Header("Spawn Timing")]
    public float firstDelay = 3f;
    public float spawnInterval = 10f;

    [Header("Spawn Areas")]
    public BoxCollider2D topArea;    // assign a BoxCollider2D for Player 2 zone
    public BoxCollider2D bottomArea; // assign a BoxCollider2D for Player 1 zone

    private bool spawnOnBottom = true;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            if (GameObject.FindWithTag("Power up") == null)
            {
                SpawnPowerUp();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnPowerUp()
    {
        // Pick random prefab
        GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

        // Decide which area to spawn in
        BoxCollider2D area = spawnOnBottom ? bottomArea : topArea;
        spawnOnBottom = !spawnOnBottom; // alternate each time

        // Pick a random point inside the box area
        Vector3 pos = GetRandomPointInBox(area);

        // Spawn
        Instantiate(prefab, pos, Quaternion.identity);
    }

    private Vector3 GetRandomPointInBox(BoxCollider2D box)
    {
        Bounds bounds = box.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, 0f);
    }
}
