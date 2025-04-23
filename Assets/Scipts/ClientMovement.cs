using UnityEngine;

public enum ClientState
{
    WalkingToQueue,
    Waiting,
    Leaving
}

public class ClientMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float waitDuration = 30f;
    public float stoppingDistance = 0.1f;

    // Private reference that will be set by ClientSpawner
    private GameObject brokenPCPrefab; 
    private GameObject droppedPC;
    
    [HideInInspector] public ClientState currentState = ClientState.WalkingToQueue;
    [HideInInspector] public float waitTimer = 0f;
    [HideInInspector] public int issue = 0;
    
    private Vector3 targetPosition;
    private Transform spawnPoint;
    private ClientQueueManager queueManager;

    void Update()
    {
        switch (currentState)
        {
            case ClientState.WalkingToQueue:
                MoveTowardsTarget();
                if (ReachedTarget())
                {
                    if (queueManager.GetFirstClient() == this)
                    {
                        StartWaiting();
                    }
                }
                break;
                
            case ClientState.Waiting:
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitDuration && queueManager.GetFirstClient() == this)
                {
                    StartLeaving();
                }
                break;
                
            case ClientState.Leaving:
                MoveTowardsTarget();
                if (ReachedTarget())
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    public void SetQueueManager(ClientQueueManager manager)
    {
        queueManager = manager;
    }

    public void setIssue(int newIssue)
    {
        issue = newIssue;
    }

    public bool ReachedTarget()
    {
        return Vector3.Distance(transform.position, targetPosition) < stoppingDistance;
    }

    public void StartWaiting()
    {
        currentState = ClientState.Waiting;
        waitTimer = 0f;

        DropBrokenPC();
    }

    public void StartLeaving()
    {
        Destroy(droppedPC);
        currentState = ClientState.Leaving;
        SetTarget(spawnPoint.position);
        queueManager.RemoveClient(this);
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }

    public void SetSpawnPoint(Transform point)
    {
        spawnPoint = point;
    }

    public void DropBrokenPC()
    {
        if (brokenPCPrefab != null)
        {
            Vector3 dropPosition = transform.position + transform.forward * 5f;
            droppedPC = Instantiate(brokenPCPrefab, dropPosition, Quaternion.identity);
            
            Debug.Log($"Spawned PC: {droppedPC.name} at {dropPosition}");
            Debug.Log($"PC active: {droppedPC.activeSelf}, layer: {droppedPC.layer}");

            Computer computer = droppedPC.GetComponent<Computer>();
            if (computer != null)
            {
                computer.ownerClient = this;
                computer.issueType = issue;
                Debug.Log($"Computer component added! Issue: {computer.issueType}");
            }
            else
            {
                Debug.LogError("No Computer component found on prefab!");
            }
            
            droppedPC.name = issue == 0 ? "brokenHW" : "brokenSW";
        }
    }


    // Add this method to set the prefab reference
    public void SetBrokenPCPrefab(GameObject prefab)
    {
        brokenPCPrefab = prefab;
    }


}