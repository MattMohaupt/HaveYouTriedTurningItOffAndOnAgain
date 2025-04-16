using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInfoManeger : MonoBehaviour
{
    [SerializeField] private float totalTime;
    [SerializeField] private TextMeshProUGUI timerText; 
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI resultText; 
    private int money = 100;

    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject resultUI;

    public static GameInfoManeger instance;

    private float currentTime;
    private bool isRunning = true;

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                resultText.text = string.Format("You earned {0}$ today", money);
                ShowResult();
            }
            UpdateTimerUI();
            UpdateMoneyUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("Remaining Time: {0:00}:{1:00}", minutes, seconds);
    }

    void UpdateMoneyUI()
    {
        moneyText.text = string.Format("Money: {0}$", money);
    }

    void ShowResult()
    {   
        gameUI.SetActive(false);
        resultUI.SetActive(true);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}