using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int StartingHealth = 5;
    private int _health;

    public float InvincibilityDuration = 1;

    public float FlashFrequency = 4;

    private float _invincibilityTimer;

    private SpriteRenderer _playerSpriteRenderer;



    // Start is called before the first frame update
    void Start()
    {
        var player = FindObjectOfType<PlayerController>();

        if (player != null)
        {
            _playerSpriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
        }
        _health = StartingHealth;    
    }

    // Update is called once per frame
    void Update()
	{
		if (_invincibilityTimer >= 0)
		{
			_invincibilityTimer -= Time.deltaTime;

			if (_playerSpriteRenderer != null)
			{
				float a = SawWave(1.0f - (_invincibilityTimer / InvincibilityDuration), FlashFrequency);

				Color playerColor = _playerSpriteRenderer.color;
				_playerSpriteRenderer.color = new Color(playerColor.r, playerColor.g, playerColor.b, a);

				if (_invincibilityTimer <= 0)
				{
					_playerSpriteRenderer.color = new Color(playerColor.r, playerColor.g, playerColor.b, 1.0f);
				}
			}


		}
	}

	private float SawWave(float t, float frequency)
    {
        float duration = 1.0f / frequency;

        var saw = Mathf.Repeat(t, duration) / duration;

        return saw*saw;
    }

    public void Hit(int damage)
    {
        if (_invincibilityTimer > 0)
            return;

        _health -= damage;
        FindObjectOfType<ScreenShake>().StartShake(0.2f, 0.25f);

        if (_health < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            _invincibilityTimer = InvincibilityDuration;
        }
    }
}
