using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCannon : MonoBehaviour
{
    public GameObject Projectile;

    public float MinRate, MaxRate;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;

            if (_timer < 0)
            {
                SpawnProjectile();
                ResetTimer();
            }
        }

    }

    private void SpawnProjectile()
    {
        GameObject.Instantiate(Projectile, this.transform.position, this.transform.rotation);
    }

    private void ResetTimer()
    {
        _timer = Random.Range(MinRate, MaxRate);
    }
}
