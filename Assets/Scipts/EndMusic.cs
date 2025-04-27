using UnityEngine;

public class EndMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;

    private GameObject EndingUI;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        EndingUI = GetComponent<GameObject>();
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
  //   if (EndingUI.activeSelf == true) {
  //       PlayMusic();
  //   }
  //   else {
  //       StopMusic();
  //   }
  // }
}
