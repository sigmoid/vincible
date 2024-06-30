using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Lifetime = 1.0f;

    public Vector3 Direction = Vector3.up;

    public float Velocity = 10;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = Lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
            Destroy(this.gameObject);

        transform.position += Direction * Velocity * Time.deltaTime;
    }
}
