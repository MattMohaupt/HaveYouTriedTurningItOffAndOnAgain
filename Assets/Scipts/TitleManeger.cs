using UnityEngine;
using UnityEngine.UI;

public class TitleManeger : MonoBehaviour
{
    [SerializeField] private Button play_btn;
    [SerializeField] private Button credit_btn;
    [SerializeField] private Button intruction_btn;
    [SerializeField] private Button quit_btn;
    [SerializeField] private GameObject startingUI;
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject joinUI;
    [SerializeField] private GameObject creditUI;
    [SerializeField] private GameObject intructionUI;

    public MenuMusic music;

    void Start()
    {
        // music.PlayMusic();

        play_btn.onClick.AddListener(() =>
        {
            titleUI.SetActive(false);
            joinUI.SetActive(true);
        });

        credit_btn.onClick.AddListener(() =>
        {
            startingUI.SetActive(false);
            creditUI.SetActive(true);
        });

        intruction_btn.onClick.AddListener(() =>
        {
            startingUI.SetActive(false);
            intructionUI.SetActive(true);
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
