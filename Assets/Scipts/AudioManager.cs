using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject menuUI;
    public GameObject currentUI;
    public GameObject resultUI;

    public StartMusic startMusic;
    public BackgroundMusic gameMusic;
    public EndMusic endMusic;
    public GameObject sounds;

    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuUI.activeSelf) {
            // Debug.Log("playing start music");
            startMusic.PlayMusic();
            gameMusic.StopMusic();
            endMusic.StopMusic();

            if (sounds.activeSelf) {
                sounds.SetActive(false);
            }
        }

        if (currentUI.activeSelf) {
            // Debug.Log("playing game music");
            startMusic.StopMusic();
            gameMusic.PlayMusic();
            endMusic.StopMusic();

            if (!sounds.activeSelf) {
                sounds.SetActive(true);
            }
        }

        if (resultUI.activeSelf) {
            // Debug.Log("playing end music");
            startMusic.StopMusic();
            gameMusic.StopMusic();
            endMusic.PlayMusic();

            if (sounds.activeSelf) {
                sounds.SetActive(false);
            }
        }
    }
}
