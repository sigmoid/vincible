using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strobe : MonoBehaviour
{
    public TMPro.TMP_Text Text;
    public UnityEngine.UI.Image Image;

    public float FlashRate = 1;

    private float _flashTimer;

    private bool _currentStrobe = true;

    public Color StrobeColor, DarkColor;

    // Start is called before the first frame update
    void Start()
    {
        _flashTimer = 1.0f / FlashRate;    
    }

    // Update is called once per frame
    void Update()
    {
        if (_flashTimer > 0)
        {
            _flashTimer -= Time.deltaTime;

            if (_flashTimer <= 0)
            {
                Switch();
                _flashTimer = 1.0f / FlashRate;
            }
        }
    }

    void Switch()
    {
        _currentStrobe = !_currentStrobe;
        if (_currentStrobe)
        {
            Text.color = StrobeColor;
            Image.color = DarkColor;
        }
        else
        {
            Text.color = DarkColor;
            Image.color = StrobeColor;
        }
    }
}
