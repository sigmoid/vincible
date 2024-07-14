using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMPro.TMP_Text ScoreText;

    public string MenuScene;

    public InitialSelector ScoreNameChar1;
	public InitialSelector ScoreNameChar2;
	public InitialSelector ScoreNameChar3;

	private int _score;
    // Start is called before the first frame update
    void Start()
    {
        _score = PlayerPrefs.GetInt("LastScore");
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = _score.ToString();

        if (Input.GetButtonDown("Fire3"))
        {
            string scoreName = ScoreNameChar1.GetChar() + ScoreNameChar2.GetChar() + ScoreNameChar3.GetChar();
           
            FindObjectOfType<ScoreDB>().SendScore(scoreName, _score, success => { 
                SceneManager.LoadScene(MenuScene);    
            });
        }
    }
}
