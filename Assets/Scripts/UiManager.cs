using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private int score;
    public Text scoreText;


    private void OnEnable()
    {
        GamePlayManager.OnScoreUpdate += UpdateScore;
    }
    private void OnDisable()
    {
        GamePlayManager.OnScoreUpdate -= UpdateScore;
    }

    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = scoreToAdd.ToString();
    }
}
