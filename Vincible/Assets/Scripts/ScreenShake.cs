using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Vector3 _basePosition;
    private float _timer;
    private float _currentStrength;

    // Start is called before the first frame update
    void Start()
    {
        _basePosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            var randomMove = Random.insideUnitCircle * _currentStrength;
			transform.position = _basePosition + new Vector3(randomMove.x, randomMove.y, 0);
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                transform.position = _basePosition; 
            }
        }
    }

    public void StartShake(float strength, float duration)
    {
        _currentStrength = strength;
        _timer = duration;
    }
}
