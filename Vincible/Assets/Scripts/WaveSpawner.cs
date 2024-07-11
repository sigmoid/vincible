using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] Round1;
    public GameObject[] Round2;

    public GameObject RoundCanvas;
    public TMPro.TMP_Text RoundText;

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

    private float _betweenWaveTimer;

    private const float BETWEEN_WAVE_DURATION = 5;

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
        if(_betweenWaveTimer > 0)
        {
            _betweenWaveTimer -= Time.deltaTime;
            if(_betweenWaveTimer <= 0)
            {
                RoundCanvas.SetActive(false);
                _betweenWaveTimer = 0;
            }
        }
        else if (_spawnTimer > 0)
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
        _roundTimer--;

        if (_roundTimer <= 0)
        {
            _betweenWaveTimer = BETWEEN_WAVE_DURATION;
            _currentRoundIndex++;
            RoundCanvas.SetActive(true);
            int newRound = _currentRoundIndex + 1;
            RoundText.text = "Round " + newRound;
            Debug.Log("NEW WAVE: " + _currentRoundIndex);
            _currentRoundIndex = Mathf.Min(_currentRoundIndex, NumRounds);
            _lastWaveId = -1;
            _roundTimer = WavesPerRound;
            _spawnTimer = 0.1f;
            return;
        }

        int nextId = Random.Range(0, round.Length);

        while (nextId == _lastWaveId && round.Length > 1)
            nextId = Random.Range(0, round.Length);

        _lastWaveId = nextId;

		var selectedWave = round[nextId];

        if (PrevWave)
        {
            Destroy(PrevWave);
        }
        PrevWave = CurrentWave;

        CurrentWave = Instantiate(selectedWave, transform.position, Quaternion.identity);
    }

    void ResetTimer()
    {
        _spawnTimer = WaveLength / ScrollSpeed;
    }
}
