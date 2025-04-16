using UnityEngine;

public class Computer : MonoBehaviour
{
    public enum ComputerState { Broken, Fixed }
    public ComputerState currentState = ComputerState.Broken;
    
    public int rewardAmount = 100;
    public ClientMovement ownerClient;
    public int issueType; // 0 for HW, 1 for SW
    
    public void FixComputer()
    {
        if (currentState == ComputerState.Broken)
        {
            currentState = ComputerState.Fixed;
            
            // Reward the player
            GameInfoManeger.instance.AddMoney(rewardAmount);
            
            // Notify the client to leave
            if (ownerClient != null)
            {
                ownerClient.StartLeaving();
            }
            
            // Optionally change appearance or destroy this object
            Destroy(gameObject);
        }
    }
}