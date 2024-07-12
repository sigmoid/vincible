using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int ScoreAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        var scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager != null)
        {
            scoreManager.AddScore(ScoreAmount);
        }

        FindObjectOfType<PowerupManager>().CollectPowerup();

		Destroy(this.gameObject);
	}
}
