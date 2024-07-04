using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite EmptySprite;

    public int HealthThreshold;

    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        var playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth.GetCurrentHealth() < HealthThreshold && _image.sprite != EmptySprite)
        {
            _image.sprite = EmptySprite;
        }
    }
}
