using System.Collections.Generic;
using UnityEngine;

public class ClientQueueManager : MonoBehaviour
{
    public Transform queueStartPoint;
    public float spacing = 1.5f;

    private List<ClientMovement> clients = new List<ClientMovement>();

    public void AddClient(ClientMovement client)
    {
        clients.Add(client);
        UpdateQueuePositions();
    }

    public void RemoveClient(ClientMovement client)
    {
        clients.Remove(client);
        UpdateQueuePositions();
    }

    public int GetClientCount()
    {
        return clients.Count;
    }

    public ClientMovement GetFirstClient()
    {
        return clients.Count > 0 ? clients[0] : null;
    }

    private void UpdateQueuePositions()
    {
        for (int i = 0; i < clients.Count; i++)
        {
            Vector3 position = GetQueuePosition(i);
            clients[i].SetTarget(position);
        }
    }

    private Vector3 GetQueuePosition(int index)
    {
        return queueStartPoint.position - queueStartPoint.forward * spacing * index;
    }
}