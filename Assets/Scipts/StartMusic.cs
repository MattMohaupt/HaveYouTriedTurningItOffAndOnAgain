using UnityEngine;

public class StartMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject menuUI;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        menuUI = GetComponent<GameObject>();
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
  //   if (menuUI.activeSelf == true) {
  //     PlayMusic();
  //   }
  //   else {
  //     StopMusic();
  //   }
  // }
}
