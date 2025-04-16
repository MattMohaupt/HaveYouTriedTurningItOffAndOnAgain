using UnityEngine;
using UnityEngine.UI;

public class TitleManeger : MonoBehaviour
{
    [SerializeField] private Button play_btn;
    [SerializeField] private Button quit_btn;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject join;

    void Start()
    {
        join.SetActive(false);

        play_btn.onClick.AddListener(() =>
        {
            title.SetActive(false);
            join.SetActive(true);
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
