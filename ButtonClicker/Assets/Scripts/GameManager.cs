using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public double CurrentScore { get; private set; }
    public double ScorePerSecondMultiplier { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;

    #region Main Variables
    
    private NumberFormat _numberFormat;
    private double _multiplier;
    double _scorePerSecond;
    
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _numberFormat = GetComponent<NumberFormat>();
        CurrentScore = 0f;
        _multiplier = 1f;
        _scorePerSecond = 1f;
        ScorePerSecondMultiplier = 0f;
        
        multiplierText.text = "Per Second: " + Mathf.Round((float)ScorePerSecondMultiplier * 100f) / 100f;
    }

    private void Update()
    {
        //Update Score Text
        scoreText.text = "$" + _numberFormat.ShortNotation(CurrentScore);
        
        //Add Score Per Second Multiplier
        _scorePerSecond = ScorePerSecondMultiplier * Time.deltaTime;
        CurrentScore += _scorePerSecond;
        multiplierText.text = "Per Second: " + Mathf.Round((float)ScorePerSecondMultiplier * 100f) / 100f;
    }

    public void ButtonClick()
    {
        CurrentScore += _multiplier;
    }

    public void PurchaseUpgrade(int cost)
    {
        CurrentScore -= cost;
    }

    public void IncreaseSpsMultiplier(float amount)
    {
        ScorePerSecondMultiplier += amount;
    }
}