using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    [SerializeField] private Button rePlay_btn;
    [SerializeField] private Button quit_btn;

    public AudioSource applause;

    void Start()
    {
        applause.Play();
        
        rePlay_btn.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton != null)
            {
                if (NetworkManager.Singleton.IsHost)
                    NetworkManager.Singleton.Shutdown();
                else if (NetworkManager.Singleton.IsClient)
                    NetworkManager.Singleton.Shutdown();

                Destroy(NetworkManager.Singleton.gameObject);
            }
            Application.LoadLevel(Application.loadedLevel);
        });

        quit_btn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    void Update()
    {
        
    }
}
