using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    void Start()
    {
        this.score = 0;
        scoreText = GetComponent<TMP_Text>();
        SetScoreText(); 
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        SetScoreText();
    }
    
    void SetScoreText()
    {
        scoreText.text = "Score: " + this.score.ToString();
    }
}
