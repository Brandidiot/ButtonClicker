using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public double CurrentScore { get; private set; }
    private double _scorePerSecondMultiplier;
    private double _multiplier;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;

    private NumberFormat _numberFormat;
    private double _scorePerSecond;

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
        _scorePerSecondMultiplier = 0f;
        
        multiplierText.text = "Per Second: " + Mathf.Round((float)_scorePerSecondMultiplier * 100f) / 100f;
    }

    private void Update()
    {
        //Update Score Text
        scoreText.text = "$" + _numberFormat.ShortNotation(CurrentScore);
        
        //Add Score Per Second Multiplier
        _scorePerSecond = _scorePerSecondMultiplier * Time.deltaTime;
        CurrentScore += _scorePerSecond;
        multiplierText.text = "Per Second: " + Mathf.Round((float)_scorePerSecondMultiplier * 100f) / 100f;
        
        #region Cheats

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.K))
        {
            CurrentScore += 1000;
        }
#endif

        #endregion
    }

    public void ButtonClick()
    {
        CurrentScore += _multiplier;
    }

    public void Purchase(double cost)
    {
        CurrentScore -= cost;
    }

    public void IncreaseSpsMultiplier(float amount)
    {
        _scorePerSecondMultiplier += amount;
    }
    
    public void IncreaseMultiplier(int amount)
    {
        _multiplier *= amount;
    }
}