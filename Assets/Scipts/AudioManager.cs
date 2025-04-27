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


    // Singleton instance.
	public static AudioManager Instance = null;
	
	// Initialize the singleton instance.
	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null)
		{
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuUI.activeSelf) {
            Debug.Log("playing start music");
            startMusic.PlayMusic();
            gameMusic.StopMusic();
            endMusic.StopMusic();
        }

        if (currentUI.activeSelf) {
            Debug.Log("playing game music");
            startMusic.StopMusic();
            gameMusic.PlayMusic();
            endMusic.StopMusic();
        }

        if (resultUI.activeSelf) {
            Debug.Log("playing end music");
            startMusic.StopMusic();
            gameMusic.StopMusic();
            endMusic.PlayMusic();
        }
    }
}
