using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private Unity.Mathematics.Random rng = new Unity.Mathematics.Random();

    [SerializeField]
    private int tileCount = 10;

    [SerializeField] 
    private GameObject tilePrefab;

    [SerializeField]
    private Vector3 tileOffset;
    private Vector3 currentTilePos = Vector3.zero;

    private LevelTile[] tiles;
    private int lastTileIndex = 0;

    [SerializeField]
    Vector2Int minMaxCollectibleFrequency = new Vector2Int(5, 10);

    [SerializeField]
    Vector2Int minMaxObstacleFrequency = new Vector2Int(5, 10);

    [SerializeField]
    GameObject[] collectibleObjects;

    [SerializeField]
    GameObject[] cursedObjects;

    [SerializeField]
    GameObject[] obstacleObjects;

    [SerializeField]
    int nextCollectibleIn = 1;

    float oddsOfGoodItem = 1.0f;

    [SerializeField]
    int nextObstacleIn = 1;

    [SerializeField]
    float distanceBehindGeneration = 4.0f;

    private float tempCounter = 0;

    Runner runner;

    void Start()
    {
        GameManager.instance.Points = 0;
        runner = FindAnyObjectByType<Runner>();
        rng.InitState();
        tiles = new LevelTile[tileCount];
        for (int i = 0; i < tileCount; i++) {
            tiles[i] = new LevelTile();
            RegenerateTile(tiles[i]);
        }
    }

    private void Update()
    {
        if (runner != null && Vector3.Distance(runner.transform.position, currentTilePos) < distanceBehindGeneration * tileOffset.magnitude)
        {
            RegenerateLastTile();
        }
    }

    public void RegenerateLastTile() {
        RegenerateTile(tiles[lastTileIndex]);
        lastTileIndex = (lastTileIndex + 1) % tileCount;
    }

    void RegenerateTile(LevelTile tile) {
        if (tile.tileObject != null) {
            Destroy(tile.tileObject);
        }
        GameObject tileObject = Instantiate(tilePrefab, gameObject.transform);
        tileObject.transform.position = currentTilePos;
        
        tile.tileObject = tileObject;

        if (nextCollectibleIn <= 0 && collectibleObjects.Count() > 0) {
            GameObject collectible;
            if (Random.value <= oddsOfGoodItem)
            {
                collectible = Instantiate(collectibleObjects[rng.NextInt(collectibleObjects.Count())], tileObject.transform);
                oddsOfGoodItem -= 0.2f;
            }
            else
            {
                collectible = Instantiate(cursedObjects[rng.NextInt(cursedObjects.Count())], tileObject.transform);
                oddsOfGoodItem += 0.2f;
            }
            tile.containedObjects.Add(collectible);
            nextCollectibleIn = rng.NextInt(minMaxCollectibleFrequency.x, minMaxCollectibleFrequency.y + 1);

            CollectibleSpawnPoint[] spawnPoints = gameObject.GetComponentsInChildren<CollectibleSpawnPoint>(true);
            if (spawnPoints.Length > 0)
            {
                collectible.transform.localPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.localPosition;
            }
            else
            {
                collectible.transform.localPosition = Vector3.zero;
            }
        }
        else {
            nextCollectibleIn--;
        }

        if (nextObstacleIn <= 0 && obstacleObjects.Count() > 0)
        {
            GameObject obstacle = Instantiate(obstacleObjects[rng.NextInt(obstacleObjects.Count())], tileObject.transform);
            tile.containedObjects.Add(obstacle);
            nextObstacleIn = rng.NextInt(minMaxObstacleFrequency.x, minMaxObstacleFrequency.y + 1);

            ObstacleSpawnPoint[] spawnPoints = gameObject.GetComponentsInChildren<ObstacleSpawnPoint>(true);
            if (spawnPoints.Length > 0)
            {
                obstacle.transform.localPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.localPosition;
            }
            else
            {
                obstacle.transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            nextObstacleIn--;
        }

        currentTilePos += tileOffset;
    }
}
