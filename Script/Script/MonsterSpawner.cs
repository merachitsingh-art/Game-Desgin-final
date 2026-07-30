using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] monsterPrefabs;
    public float spawnInterval = 3f;
    public Transform[] spawnPoints; 

    private float spawnTimer;

    void Start()
    {
        // If no spawn points are set in the Inspector, grab all children automatically
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            int childCount = transform.childCount;
            spawnPoints = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                spawnPoints[i] = transform.GetChild(i);
            }
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnMonster();
            spawnTimer = 0f;
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefabs.Length == 0) return;

        // Choose a random monster
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject monsterToSpawn = monsterPrefabs[randomIndex];

        // Choose a random spawn position
        Vector3 spawnPosition = transform.position;
        if (spawnPoints.Length > 0)
        {
            int randomPoint = Random.Range(0, spawnPoints.Length);
            spawnPosition = spawnPoints[randomPoint].position;
        }

        Instantiate(monsterToSpawn, spawnPosition, Quaternion.identity);
    }
}