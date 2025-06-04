using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1f; // Start rate: 1 enemy/sec
    public float increaseInterval = 30f;
    public float maxSpawnRate = 10f;

    public Tilemap backgroundTilemap;
    public Camera mainCamera;

    private float currentRate;
    private float timeSinceLastIncrease = 0f;

    private Bounds backgroundBounds;

    void Start()
    {
        currentRate = spawnRate;

        if (backgroundTilemap != null)
        {
            backgroundBounds = backgroundTilemap.localBounds;
        }

        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        timeSinceLastIncrease += Time.deltaTime;

        if (timeSinceLastIncrease >= increaseInterval)
        {
            currentRate += 1f;
            currentRate = Mathf.Min(currentRate, maxSpawnRate);
            timeSinceLastIncrease = 0f;
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            for (int i = 0; i < currentRate; i++)
            {
                Vector2 spawnPos = GetValidSpawnPosition();
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    Vector2 GetValidSpawnPosition()
    {
        // Convert tilemap bounds to world space
        Vector3 bgMin = backgroundTilemap.CellToWorld(backgroundTilemap.cellBounds.min);
        Vector3 bgMax = backgroundTilemap.CellToWorld(backgroundTilemap.cellBounds.max);

        // Get camera world-space bounds
        Vector2 camMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0));
        Vector2 camMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));

        Vector2 spawnPos;
        int safety = 0;

        do
        {
            float x = Random.Range(bgMin.x, bgMax.x);
            float y = Random.Range(bgMin.y, bgMax.y);
            spawnPos = new Vector2(x, y);

            safety++;
            if (safety > 100) break;

        } while (spawnPos.x > camMin.x && spawnPos.x < camMax.x &&
                 spawnPos.y > camMin.y && spawnPos.y < camMax.y);

        return spawnPos;
    }
}
