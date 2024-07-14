using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupReminder : MonoBehaviour
{
    private bool _powerupConsumed;
    private bool _powerupGained;
    public GameObject ReminderText;

    public void OnPowerupConsumed()
    {
        _powerupConsumed = true;    
    }

    public void OnPowerupGained()
    {
        _powerupGained = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_powerupGained && !_powerupConsumed && !ReminderText.activeInHierarchy)
        {
            ReminderText.SetActive(true);
        }

        if (_powerupConsumed && ReminderText.activeInHierarchy)
        {
            ReminderText.SetActive(false);
        }
    }
}
