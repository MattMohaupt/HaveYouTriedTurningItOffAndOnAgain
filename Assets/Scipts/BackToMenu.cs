using UnityEngine;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{   
    [SerializeField] private Button back_btn;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject currentUI;

    void Start()
    {
        back_btn.onClick.AddListener(() =>
        {
            currentUI.SetActive(false);
            menuUI.SetActive(true);
        });

    }
    
    void Update()
    {
        
    }
}
