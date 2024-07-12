using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public TMP_Text PowerupText;
    public GameObject Popup;
    public TMP_Text PopupText;

    private int _powerupCount = 0;

    private const float POPUP_DURATION =0.125f;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        
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
                ScreenWipe();
                Popup.SetActive(false);
            }
        }   
    }

    public void CollectPowerup()
    {
        _powerupCount ++;

        PowerupText.text = "x " + _powerupCount.ToString();
    }

    public bool ConsumePowerup()
    {
        if(_powerupCount > 0)
        {
            _powerupCount--;
            PowerupText.text = "x " + _powerupCount.ToString();
            _timer=POPUP_DURATION;
            Popup.SetActive(true);
            FindObjectOfType<NegateEffect>().enabled = true;
            PopupText.text = "!!!";
            Time.timeScale = 0.0f;
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
