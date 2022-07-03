using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager Instance { get; private set; }

    public double currentScore;
    public double scorePerSecondMultiplier;
    public double multiplier;
    public double scorePerSecond;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;

    private NumberFormat _numberFormat;
    

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
        
        multiplierText.text = "Per Second: " + Mathf.Round((float)scorePerSecondMultiplier * 100f) / 100f;
    }

    private void Update()
    {
        //Update Score Text
        scoreText.text = "$" + _numberFormat.ShortNotation(currentScore);
        
        //Add Score Per Second Multiplier
        scorePerSecond = scorePerSecondMultiplier * Time.deltaTime;
        currentScore += scorePerSecond;
        multiplierText.text = "Per Second: " + Mathf.Round((float)scorePerSecondMultiplier * 100f) / 100f;
        
        #region Cheats

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.K))
        {
            currentScore += 1000;
        }
#endif

        #endregion
    }

    public void ButtonClick()
    {
        currentScore += multiplier;
    }

    public void Purchase(double cost)
    {
        currentScore -= cost;
    }

    public void IncreaseSpsMultiplier(float amount)
    {
        scorePerSecondMultiplier += amount;
    }
    
    public void IncreaseMultiplier(int amount)
    {
        multiplier *= amount;
    }

    public void LoadData(GameData data)
    {
        currentScore = data.currentScore;
        scorePerSecondMultiplier = data.scorePerSecondMultiplier;
        multiplier = data.multiplier;
        scorePerSecond = data.scorePerSecond;
    }

    public void SaveData(GameData data)
    {
        data.currentScore = (float)currentScore;
        data.scorePerSecondMultiplier = (float)scorePerSecondMultiplier;
        data.multiplier = (float)multiplier;
        data.scorePerSecond = (float)scorePerSecond;
    }
}