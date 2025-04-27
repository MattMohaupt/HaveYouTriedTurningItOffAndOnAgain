using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
}
