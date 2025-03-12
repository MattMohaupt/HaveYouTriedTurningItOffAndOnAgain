using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;
using Unity.Netcode.Transports.UTP;
public class NetworkManagerUI : MonoBehaviour
{
    // [SerializeField] attribute is used to make the private variables accessible
    // within the Unity editor without making them public
    [SerializeField] private Button host_btn;
    [SerializeField] private Button client_btn;

    //text to display the join code
    [SerializeField] private TMP_Text joinCodeText;
    // max number of players
    [SerializeField] private int maxPlayers = 4;
    // join code
    public string joinCode;

    [SerializeField] private TMP_InputField joinCodeInputField;
    // after all objectes are created and initialized
    // Awake() method is called and executed
    // Awake is always called before any Start functions.

    private void Awake()
    {
        // add a listener to the host button
        host_btn.onClick.AddListener(() =>
        {
            // call the NetworkManager's StartHost() method
            // NetworkManager.Singleton.StartHost();
            StartHostRelay();
        });

        // add a listener to the client button
        client_btn.onClick.AddListener(() =>
        {
            // call the NetworkManager's StartClient() method
            // NetworkManager.Singleton.StartClient();
            StartClientRelay(joinCodeInputField.text);
        });
    }

    private async void Start()
    {
        //initialize unity services and authentication
        await UnityServices.InitializeAsync();

        //sign in anonymously
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    // Start host relay
    public async void StartHostRelay()
    {
        Allocation allocation = null;
        try
        {
            // create allocation
            allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            // get the join code
            joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }

        // get the hosting data
        // dtls is a connection type - a type of security protocol
        var serverData = new RelayServerData(allocation, "dtls");

        // set the relay server data
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);

        // start the host
        NetworkManager.Singleton.StartHost();

        // display the join code
        joinCodeText.text = joinCode;
    }

    // start client relay
    public async void StartClientRelay(string joinCode)
    {
        JoinAllocation joinAllocation = null;

        try
        {
            // join the allocation
            joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }

        // set it on the network manager
        var serverData = new RelayServerData(joinAllocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(serverData);

        //Start the client
        NetworkManager.Singleton.StartClient();
    }

}