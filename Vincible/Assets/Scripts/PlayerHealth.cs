using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int StartingHealth = 5;
    private int _health;

    public float InvincibilityDuration = 1;

    public float FlashFrequency = 4;

    private float _invincibilityTimer;

    private SpriteRenderer _playerSpriteRenderer;

    public string GameOverScreenScene = "gameover";

    public TMP_Text GameOverText;
    public Image ScreenCoverImage;
    public GameObject UI;
    public AudioSource MusicSource;
    public AudioSource HitSource;
    public AudioSource GongSource;

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
			_invincibilityTimer -= Time.unscaledDeltaTime;

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
        HitSource.PlayOneShot(HitSource.clip);

        if (_health < 0)
        {
            PlayerPrefs.SetInt("LastScore", FindObjectOfType<ScoreManager>().GetScore());
            StartCoroutine(DeathRoutine());
        }
        else
        {
            _invincibilityTimer = InvincibilityDuration;
        }
    }

    public void StartInvincibility(float duration)
    {
        _invincibilityTimer = duration;
    }
    public int GetCurrentHealth()
    {
        return _health;
    }

    private IEnumerator DeathRoutine()
    {
        GameOverText.gameObject.SetActive(true);
        UI.SetActive(false);
        ScreenCoverImage.gameObject.SetActive(true);

        Time.timeScale = 0.2f;

        FindObjectOfType<PlayerController>().FreezeInput();
        var cannons = FindObjectsByType<BasicCannon>(FindObjectsSortMode.None);
        foreach (var cannon in cannons)
            cannon.FreezeInput();

		GameOverText.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, 0);
		ScreenCoverImage.color = new Color(ScreenCoverImage.color.r, ScreenCoverImage.color.g, ScreenCoverImage.color.b, 0);

		float fadeinDuration = 5;
        float timer = fadeinDuration;
        float originalVolume = MusicSource.volume;

        while (timer > 0)
        {
			MusicSource.volume = originalVolume * (timer / fadeinDuration);
			float t = Mathf.Sqrt(1.0f - (timer / fadeinDuration));
            timer -= Time.unscaledDeltaTime;
            GameOverText.color = new Color(GameOverText.color.r, GameOverText.color.g, GameOverText.color.b, t);
            yield return null;
        }

        float fadeOutDuration = 2;
        timer = fadeOutDuration;
        originalVolume = GongSource.volume;

		while (timer > 0)
		{
            GongSource.volume = originalVolume * (timer / fadeOutDuration);
			timer -= Time.unscaledDeltaTime;
			ScreenCoverImage.color = new Color(ScreenCoverImage.color.r, ScreenCoverImage.color.g, ScreenCoverImage.color.b, 1.0f - (timer / fadeOutDuration));
			yield return null;
		}

        Time.timeScale = 1.0f;

		SceneManager.LoadScene(GameOverScreenScene);
		yield break;
	}
}
