using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BettingManager : MonoBehaviour
{

    static BettingManager instance;

    public int currentBet = 1;
    //public int maxBet = 1000000;
    //public int minBet = 10;
    public int totalCredits = 10000000;
    public Text betText;
    public Text totalCreditsText;

    public static BettingManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

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
        currentBet = 10;
        totalCredits = 10000000;
        UpdateUI();

    }

    public void UpdateUI()
    {
        betText.text = $"{currentBet}";
        totalCreditsText.text = $"{totalCredits}";
    }
    public void IncreaseBet()
    {
        switch (currentBet)
        {
            case 10:
                if(totalCredits>50) currentBet = 50;
                break;
            case 50:
                if (totalCredits > 100) currentBet = 100;
                break;
            case 100:
                if (totalCredits > 500) currentBet = 500;
                break;
            case 500:
                if (totalCredits > 1000) currentBet = 1000;
                break;
            case 1000:
                if (totalCredits > 5000) currentBet = 5000;
                break;
            case 5000:
                if (totalCredits > 10000) currentBet = 10000;
                break;
            case 10000:
                if (totalCredits > 50000) currentBet = 50000;
                break;
            case 50000:
                if (totalCredits > 100000) currentBet = 100000;
                break;
            case 100000:
                if (totalCredits > 500000) currentBet = 500000;
                break;
            case 500000:
                if (totalCredits > 1000000) currentBet = 1000000;
                break;
        }

        UpdateUI();
    }
    public void DecreaseBet()
    {
        
        switch (currentBet)
        {
            case 1000000:
                currentBet = 500000;
                break;
            case 500000:
                currentBet = 100000;
                break;
            case 100000:
                currentBet = 50000;
                break;
            case 50000:
                currentBet = 10000;
                break;
            case 10000:
                currentBet = 5000;
                break;
            case 5000:
                currentBet = 1000;
                break;
            case 1000:
                currentBet = 500;
                break;
            case 500:
                currentBet = 100;
                break;
            case 100:
                currentBet = 50;
                break;
            case 50:
                currentBet = 10;
                break;

        }
        UpdateUI();
    }
    public void Spin()
    {
        totalCredits -= currentBet;
        if (totalCredits < 0)
        {
            totalCredits = 0;
        }
        if (totalCredits < currentBet)
        {
            while (currentBet > totalCredits)
            {
                DecreaseBet();
                if (currentBet <= 10)
                {
                    break;
                }
            }
        }
        UpdateUI();
    }
    public bool IsRequireCredit()
    {
        return totalCredits >= currentBet;
    }
}
