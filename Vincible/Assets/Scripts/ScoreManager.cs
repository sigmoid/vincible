using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TMP_Text ScoreText;

    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        _score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        ScoreText.text = _score.ToString();
    }

    public int GetScore()
    {
        return _score; 
    }
}
