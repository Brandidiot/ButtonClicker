using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private NumberFormat _numberFormat;
    [SerializeField] private double _currentScore;
    private double _multiplier;

    private void Start()
    {
        _numberFormat = GetComponent<NumberFormat>();
        _currentScore = 0;
        _multiplier = 1;
    }

    private void Update()
    {
        scoreText.text = "$" + _numberFormat.ShortNotation(_currentScore);
    }

    public void ButtonClick()
    {
        _currentScore += _multiplier;
    }
    
}