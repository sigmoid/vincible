using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
    public List<GameObject> Patterns;
}
public class WaveSpawner : MonoBehaviour
{
    public List<Wave> Rounds;

    public GameObject RoundCanvas;
    public TMPro.TMP_Text RoundText;

    public int WavesPerRound = 10;

    public float ScrollSpeed = 1.8f;

    public float WaveLength;


    private GameObject PrevWave, CurrentWave;

    private int _currentRoundIndex = 0;

    private int _roundTimer;

    private int _lastWaveId = -1;


    private const float BETWEEN_WAVE_DURATION = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        _roundTimer = WavesPerRound;
        StartCoroutine(WaveSpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnWave()
    {
        var round = Rounds[_currentRoundIndex];

        int nextId = Random.Range(0, round.Patterns.Count);

        while (nextId == _lastWaveId && round.Patterns.Count > 1)
            nextId = Random.Range(0, round.Patterns.Count);

        _lastWaveId = nextId;

		var selectedWave = round.Patterns[nextId];

        if (PrevWave)
        {
            Destroy(PrevWave);
        }
        PrevWave = CurrentWave;

        CurrentWave = Instantiate(selectedWave, transform.position, Quaternion.identity);
    }

    void StartNewRound()
    {
		_currentRoundIndex++;
		_currentRoundIndex = Mathf.Min(_currentRoundIndex, Rounds.Count - 1);
		_lastWaveId = -1;
		_roundTimer = WavesPerRound;
	}

    private IEnumerator WaveSpawnRoutine()
    {
        while (true)
        {
			var waveDuration = WaveLength / ScrollSpeed;

            _roundTimer--;

            if (_roundTimer <= 0)
            {
				yield return new WaitForSeconds(waveDuration * 2);
				int newRound = _currentRoundIndex + 1;
				RoundCanvas.SetActive(true);
				RoundText.text = "Round " + newRound;
				yield return new WaitForSeconds(BETWEEN_WAVE_DURATION);
				RoundCanvas.SetActive(false);
                StartNewRound();
                SpawnWave();
            }

            yield return new WaitForSeconds(waveDuration);
            SpawnWave();
        }
    }
}
