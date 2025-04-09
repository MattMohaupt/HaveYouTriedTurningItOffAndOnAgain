using UnityEngine;
using System.Collections.Generic;

public class ClientSpawner : MonoBehaviour
{
    public GameObject clientPrefab; // Prefab of clients to spawn
    public Transform spawnPoint; // where clients spawn
    public Transform queueStartPoint; // where the waiting line begins
    public float spacing = 1.5f; // space between clients in the line
    public float spawnInterval; // time interval of client spawn (SET MANULLY in the Unity tool)


    private float timer = 0f;


    // the queue that contains position of clients in the line
    private List<Transform> positionQueue = new List<Transform>(); 


    // the queue that contains all client GameObject spawned by this script.
    // this queue is not used yet, but would be helpful for implementing other behaviors later.
    private List<GameObject> clientQueue = new List<GameObject>();


    // start with an initial client spawn
    void Start() {
        UpdateQueuePositions(0);
        SpawnClient();
    }


    // spawn client every 'spawnInterval' seconds
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnClient();
            timer = 0f;
        }
    }


    void SpawnClient()
    {
        // spawn a client prefab into spawnPoint position.
        GameObject client = Instantiate(clientPrefab, spawnPoint.position, spawnPoint.rotation);

        // decide whether that client has hardware issue or software issue in 50:50 chance.
        Renderer renderer = client.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color randomColor;
            int issue;
            if (Random.value < 0.5f) {
                randomColor = Color.red;
                issue = 0;
            } else {
                randomColor = Color.blue;
                issue = 1;
            }
            renderer.material.color = randomColor;
            client.GetComponent<ClientMovement>().setIssue(issue);
        }

        // add that client GameObject instance to the queue.
        clientQueue.Add(client);

        // add the new position to the queue.
        int index = positionQueue.Count;
        UpdateQueuePositions(index + 1);

        Transform targetPos = positionQueue[index];
        client.GetComponent<ClientMovement>().SetTarget(targetPos);

        client.name = "Client_" + index;
    }
    
    void UpdateQueuePositions(int count)
    {
        positionQueue.Clear();

        for (int i = 0; i < count; i++)
        {
            GameObject point = new GameObject("QueuePos_" + i);
            point.transform.position = queueStartPoint.position - queueStartPoint.forward * spacing * i;
            positionQueue.Add(point.transform);
        }
    }
}
