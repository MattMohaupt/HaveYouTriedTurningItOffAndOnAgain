using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    public ClientQueueManager queueManager;
    public GameObject clientPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 5f;
    public int limit = 10;

    private float timer = 0f;

    void Start()
    {
        SpawnClient();
    }

    void Update()
    {
        if (queueManager.GetClientCount() < limit)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnClient();
                timer = 0f;
            }
        }
    }

    void SpawnClient()
    {
        // int randInd = Random.Range(0, clientPrefab.Length);     // should be either 0 or 1
        GameObject client = Instantiate(clientPrefab, spawnPoint.position, spawnPoint.rotation);
        ClientMovement clientMovement = client.GetComponent<ClientMovement>();

        // Assign random color and issue
        Renderer renderer = client.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color randomColor = Random.value < 0.5f ? Color.red : Color.blue;
            renderer.material.color = randomColor;
            clientMovement.setIssue(randomColor == Color.red ? 0 : 1);
        }

        // Set references
        // clientMovement.setIssue(randInd);
        clientMovement.SetSpawnPoint(spawnPoint);
        clientMovement.SetQueueManager(queueManager);

        // Add to queue
        queueManager.AddClient(clientMovement);
        client.name = "Client_" + queueManager.GetClientCount();
    }
}