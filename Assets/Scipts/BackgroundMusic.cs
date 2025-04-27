using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject currentUI;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentUI = GetComponent<GameObject>();
        // PlayMusic();
    }

    public void PlayMusic()
    {
      if (!audioSource.isPlaying)
      {
        audioSource.Play();
      }
    }
  
  public void StopMusic()
  {
    if (audioSource.isPlaying)
    {
      audioSource.Stop();
    }
  }

  // void Update() {
  //   if (currentUI.activeSelf == true) {
  //     PlayMusic();
  //   }
  //   else {
  //     StopMusic();
  //   }
  // }
}
