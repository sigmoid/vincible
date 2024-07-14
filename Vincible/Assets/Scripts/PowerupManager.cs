using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public TMP_Text PowerupText;
    public GameObject Popup;
    public TMP_Text PopupText;

    public Material ScreenEffectMaterial;
    public AudioLowPassFilter _musicFilter;

    private int _powerupCount = 0;

    private const float POPUP_DURATION =4.0f;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
		ScreenEffectMaterial.SetInt("_isActive", 0);
	}

    // Update is called once per frame
    void Update()
    {
        if(_timer > 0)
        {
            _timer -= Time.unscaledDeltaTime;

            if(_timer<=0)
            {
                Time.timeScale = 1;
                ScreenEffectMaterial.SetInt("_isActive", 0);
                _musicFilter.enabled = false;
                Popup.SetActive(false);
            }
        }   
    }

    public void CollectPowerup()
    {
        _powerupCount ++;

        PowerupText.text = "x " + _powerupCount.ToString();
        FindObjectOfType<PowerupReminder>()?.OnPowerupGained();
    }

    public bool ConsumePowerup()
    {
        if(_powerupCount > 0)
        {
            _powerupCount--;
            PowerupText.text = "x " + _powerupCount.ToString();
            _timer=POPUP_DURATION;
            Popup.SetActive(true);
            PopupText.text = "!!!";
            FindObjectOfType<PlayerHealth>().StartInvincibility(POPUP_DURATION + 1.0f);
            Time.timeScale = 0.3f;
            ScreenEffectMaterial.SetInt("_isActive", 1);
            FindObjectOfType<PowerupReminder>()?.OnPowerupConsumed();
            StartCoroutine(FadeAudioFreq());
            StartCoroutine(FadeAudioQ(0.7f, 5.5f));
            return true;
        }
        return false;
    }

    private void ScreenWipe()
    {
        var projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");

        foreach(var projectile in projectiles)
        {
            GameObject.Destroy(projectile);
        }
    }

    private float Tween(float inVal)
    {
       return Mathf.Pow(inVal, 3); 
    }

	private float TweenBack(float inVal)
	{
        var t = 1.0f - inVal;
		return Mathf.Pow(t, 3);
	}

	private IEnumerator FadeAudioFreq()
    {
        _musicFilter.enabled = true;

        var startFreq = _musicFilter.cutoffFrequency;
        float minCutoff = 54;
        _musicFilter.cutoffFrequency = minCutoff;

        float timer = POPUP_DURATION;

        while (timer > 0)
        {
            timer -= Time.unscaledDeltaTime;

            float t = 1.0f - timer / POPUP_DURATION;

            t = Tween(t);

            _musicFilter.cutoffFrequency = Mathf.Lerp(minCutoff, 22000, t);
            yield return null;
        }

        _musicFilter.enabled = false;
    }

    private IEnumerator FadeAudioQ(float min, float max)
    {

        var startFreq = _musicFilter.lowpassResonanceQ;
        _musicFilter.lowpassResonanceQ = min;

        float duration = POPUP_DURATION / 2.0f;

		float timer = duration;

        while (timer > 0)
        {
			timer -= Time.unscaledDeltaTime;

			float t = 1.0f - timer / (duration);

			_musicFilter.lowpassResonanceQ = Mathf.Lerp(min, max, t);
			yield return null;
		}

        timer = duration;

		while (timer > 0)
		{
			timer -= Time.unscaledDeltaTime;

			float t = 1.0f - timer / duration;

            t = TweenBack(t);

			_musicFilter.lowpassResonanceQ = Mathf.Lerp(max,min, t);
			yield return null;
		}

    }
}
