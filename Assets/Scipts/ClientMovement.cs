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
    }

    public void StartLeaving()
    {
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
}