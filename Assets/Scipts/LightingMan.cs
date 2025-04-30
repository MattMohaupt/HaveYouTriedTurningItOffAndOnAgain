using System.Collections;
using UnityEngine;

public class LightingMan : MonoBehaviour
{
    [SerializeField] public Light directionalLight;
    [SerializeField] private GameObject warningText; 
    public bool lightsAreOff = false;
    private bool inGracePeriod = false;
    private Coroutine toggleRoutine;
    private float timeUntilNextToggle;
    public AudioSource generatorOff;
    public AudioSource electricity;

    private void Start()
    {
        timeUntilNextToggle = 10f + Random.Range(5f, 15f);
        toggleRoutine = StartCoroutine(ToggleLightRoutine());
    }

    private IEnumerator ToggleLightRoutine()
    {
        while (true)
        {
            // Count down only when not in grace period
            if (!inGracePeriod && timeUntilNextToggle > 0)
            {
                timeUntilNextToggle -= Time.deltaTime;

                if (timeUntilNextToggle < 3) {
                    generatorOff.Play();
                }
                
                if (timeUntilNextToggle <= 5)
                {
                    electricity.Play();
                    warningText.SetActive(true);
                }
                
                if (timeUntilNextToggle <= 0)
                {
                    warningText.SetActive(false);
                    if (directionalLight != null)
                    {
                        directionalLight.enabled = false;
                        lightsAreOff = true;
                        inGracePeriod = true;
                    }
                    // Reset timer for next toggle
                    timeUntilNextToggle = 10f + Random.Range(5f, 15f);
                }
            }
            yield return null;
        }
    }

    public void TurnLightsOn()
    {
        if (directionalLight != null)
        {
            directionalLight.enabled = true;
            lightsAreOff = false;
            inGracePeriod = false;
            
            // Start grace period and reset timer
            StartCoroutine(GracePeriodRoutine());
            timeUntilNextToggle = 10f + Random.Range(5f, 15f); // Reset countdown
        }
    }

    private IEnumerator GracePeriodRoutine()
    {
        inGracePeriod = true;
        yield return new WaitForSeconds(10f); // 10 second grace period
        inGracePeriod = false;
    }

    public bool AreLightsOff()
    {
        return lightsAreOff;
    }

    private void OnDisable()
    {
        if (toggleRoutine != null)
        {
            StopCoroutine(toggleRoutine);
        }
    }
}