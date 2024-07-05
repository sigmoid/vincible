using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] Round1;
    public GameObject[] Round2;

    public int WavesPerRound = 10;

    public float ScrollSpeed = 1.8f;

    public float WaveLength;

    private float _spawnTimer;

    private GameObject PrevWave, CurrentWave;

    private int _currentRoundIndex = 0;

    private List<GameObject[]> Rounds;

    private int _roundTimer;

    private int NumRounds = 2;

    private int _lastWaveId = -1;

    // Start is called before the first frame update
    void Start()
    {
        _roundTimer = WavesPerRound;
        Rounds = new List<GameObject[]>() { Round1, Round2};
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
        var round = Rounds[_currentRoundIndex];

        int nextId = Random.Range(0, round.Length);

        while (nextId == _lastWaveId)
            nextId = Random.Range(0, round.Length);

        _lastWaveId = nextId;

		var selectedWave = round[nextId];

        if (PrevWave)
        {
            Destroy(PrevWave);
        }
        PrevWave = CurrentWave;

        CurrentWave = Instantiate(selectedWave, transform.position, Quaternion.identity);

        _roundTimer--;

        if (_roundTimer <= 0)
        {
            _currentRoundIndex++;
            Debug.Log("NEW WAVE: " + _currentRoundIndex);
            _currentRoundIndex = Mathf.Min(_currentRoundIndex, NumRounds);
            _lastWaveId = -1;
            _roundTimer = WavesPerRound;
        }
    }

    void ResetTimer()
    {
        _spawnTimer = WaveLength / ScrollSpeed;
    }
}
