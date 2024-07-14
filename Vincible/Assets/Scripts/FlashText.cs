using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class FlashText : MonoBehaviour
{
    public float FlashRate = 1;
    public TMP_Text Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float duration = 1.0f / FlashRate;
        float currentTime = Mathf.Repeat(Time.unscaledTime, duration);
        currentTime /= duration;


        float a = (currentTime > 0.5f) ? 1.0f : 0.0f;

        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, a);
    }
}
