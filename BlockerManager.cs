using System.Collections.Generic;
using UnityEngine;

public class BlockerManager : MonoBehaviour
{
    public GameObject[] blockerPrefabs;
    public int maxBlockers = 10;
    private List<GameObject> blockers = new List<GameObject>();

    public float spawnDelay = 2f;
    private float spawnTimer = 0f;

    public PlayerController playerController;
    private float minRoadX;
    private float maxRoadX;

    private void Start()
    {
        minRoadX = -playerController.roadWidth + 6f;
        maxRoadX = playerController.roadWidth - 6f;

        for (int i = 0; i < maxBlockers; i++)
        {
            GameObject blocker = Instantiate(blockerPrefabs[Random.Range(0, blockerPrefabs.Length)]);
            blocker.SetActive(false);
            blockers.Add(blocker);
        }
    }

    private void FixedUpdate()
    {
        spawnTimer -= Time.fixedDeltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnBlocker();
            spawnTimer = spawnDelay;
        }

        MoveBlockers();
    }

    void SpawnBlocker()
{
    List<GameObject> availableBlockers = blockers.FindAll(blocker => !blocker.activeSelf);
    if (availableBlockers.Count == 0)
        return;

    int randomIndex = Random.Range(0, availableBlockers.Count);
    GameObject selectedBlocker = availableBlockers[randomIndex];

    float randomX = Random.Range(minRoadX, maxRoadX);
    Vector3 spawnPosition = new Vector3(randomX, transform.position.y, playerController.transform.position.z + 100f);
    Quaternion spawnRotation = Quaternion.identity;

    selectedBlocker.transform.position = spawnPosition;
    selectedBlocker.transform.rotation = spawnRotation;
    selectedBlocker.SetActive(true);
}


    void MoveBlockers()
    {
        foreach (GameObject blocker in blockers)
        {
            if (blocker.activeSelf)
            {
                blocker.transform.Translate(Vector3.back * (playerController.speed * 4f) * Time.fixedDeltaTime); // Increase movement speed
                if (blocker.transform.position.z < playerController.transform.position.z - 5f)
                {
                    GameManage gameManage = FindObjectOfType<GameManage>();
                    gameManage.updateScore();
                    blocker.SetActive(false);
                }
            }
        }
    }
public void UpdateSpawnVolume(float amount, float maxVolume)
{
    float randomValue = Random.Range(-1f, 1f); 
    float changeAmount = randomValue < 0 ? -0.3f : 0.3f; 

    spawnDelay += changeAmount; 

    spawnDelay = Mathf.Max(spawnDelay - amount, maxVolume); 
}

}
