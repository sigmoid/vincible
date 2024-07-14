using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public TMP_Text PowerupText;
    public GameObject Popup;
    public TMP_Text PopupText;

    public Material ScreenEffectMaterial;

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
}
