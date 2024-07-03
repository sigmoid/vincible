using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] Waves;

    public float ScrollSpeed = 1.8f;

    public float WaveLength;

    private float _spawnTimer;

    private GameObject PrevWave, CurrentWave;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnTimer > 0)
        {
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0)
            {
                SpawnWave();
                ResetTimer();
            }
        }
    }

    void SpawnWave()
    {
        var selectedWave = Waves[Random.Range(0, Waves.Length)];

        Destroy(PrevWave);

        PrevWave = CurrentWave;

        CurrentWave = Instantiate(selectedWave, transform.position, Quaternion.identity);
    }

    void ResetTimer()
    {
        _spawnTimer = WaveLength / ScrollSpeed;
    }
}
