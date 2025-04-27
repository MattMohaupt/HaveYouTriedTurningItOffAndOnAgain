using UnityEngine;

public class MenuListener : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created'

    public AudioListener listen;
    public GameObject StartingUI;
    void Start()
    {
        listen = GetComponent<AudioListener>();
        listen.enabled = true;
    }

    private void On() {
        if (listen.enabled == false) {
            listen.enabled = true;
        }
    }

    private void Off() {
        if (listen.enabled == true) {
            listen.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StartingUI.activeSelf == true) {
            On();
        }
        else {
            Off();
        }
    }
}
