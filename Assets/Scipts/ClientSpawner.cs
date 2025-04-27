using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    public ClientQueueManager queueManager;
    public GameObject clientPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 5f;
    public int limit = 10;

    // public AudioSource outOfTime;

    private bool atLimit;

    private float timer = 0f;

    public GameObject brokenPCHW; // Reference to HW broken PC prefab
    public GameObject brokenPCSW; // Reference to SW broken PC prefab

    void Start()
    {
        atLimit = false;
        SpawnClient();
    }

    void Update()
    {
        if (queueManager.GetClientCount() == limit) {
            atLimit = true;
        }


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
        GameObject client = Instantiate(clientPrefab, spawnPoint.position, spawnPoint.rotation);
        ClientMovement clientMovement = client.GetComponent<ClientMovement>();

        // Assign random color and issue
        Renderer renderer = client.GetComponent<Renderer>();
        if (renderer != null)
        {
            bool isHW = Random.value < 0.5f;
            Color clientColor = isHW ? Color.red : Color.blue;
            renderer.material.color = clientColor;
            
            // Set the issue type and corresponding PC prefab
            if (isHW)
            {
                clientMovement.setIssue(0); // HW
                clientMovement.SetBrokenPCPrefab(brokenPCHW);
            }
            else
            {
                clientMovement.setIssue(1); // SW
                clientMovement.SetBrokenPCPrefab(brokenPCSW);
            }
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