using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public double CurrentScore { get; private set; }
    public double ScorePerSecondMultiplier { get; private set; }
    public double Multiplier { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;

    private NumberFormat _numberFormat;
    double _scorePerSecond;

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
        Multiplier = 1f;
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
        
        #region Cheats

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            CurrentScore += 1000;
        }
#endif

        #endregion
    }

    public void ButtonClick()
    {
        CurrentScore += Multiplier;
    }

    public void Purchase(int cost)
    {
        CurrentScore -= cost;
    }

    public void IncreaseSpsMultiplier(float amount)
    {
        ScorePerSecondMultiplier += amount;
    }
    
    public void IncreaseMultiplier(int amount)
    {
        Multiplier *= amount;
    }
}