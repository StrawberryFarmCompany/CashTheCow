using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingManager : MonoBehaviour
{

    static BettingManager instance;

    public float currentBet = 1f;
    public float maxBet = 1000000f;
    public float minBet = 10f;
    public float TotalCredits = 10000000f;
    public Text betText;
    public Text totalCreditsText;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentBet = minBet;
        TotalCredits = 10000000f;
        UpdateUI();

    }

    void Update()
    {
        
    }
    public void UpdateUI()
    {
        betText.text = $"{currentBet} Credit";
        totalCreditsText.text = $"{TotalCredits} Credit";
    }
    public void IncreaseBet()
    {
        if (currentBet < maxBet)
        {
            currentBet *= 2f;
            if (currentBet > TotalCredits)
            {
                currentBet = TotalCredits;
            }
            UpdateUI();
        }
    }
    public void DecreaseBet()
    {
        if (currentBet > minBet)
        {
            currentBet /= 2f;
            if (currentBet < minBet)
            {
                currentBet = minBet;
            }
            UpdateUI();
        }
    }
}
