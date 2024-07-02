using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonUtils : MonoBehaviour
{
    public float ScreenHeight = 10;
    public float ScreenWidth = 10;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanFireCannon(Transform shipTransform)
    {
        if (shipTransform.position.x < ScreenWidth && shipTransform.position.x > -ScreenWidth && shipTransform.position.y < ScreenHeight && shipTransform.position.y > -ScreenHeight)
            return true;

        return false;
    }
}
