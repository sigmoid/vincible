using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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


	private const float BETWEEN_WAVE_DURATION = 2.0f;
	private const float POST_WAVE_COOLDOWN_DURATION = 5.0f;

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

    private bool IsScreenClear()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }


    private IEnumerator WaveSpawnRoutine()
    {
        while (true)
        {
			var waveDuration = WaveLength / ScrollSpeed;

            _roundTimer--;

            if (_roundTimer <= 0)
            {
                while (!IsScreenClear())
                {
                    yield return null;
                }
				yield return new WaitForSeconds(POST_WAVE_COOLDOWN_DURATION);
				int newRound = _currentRoundIndex + 2;
				RoundCanvas.SetActive(true);
				RoundText.text = "Round " + newRound;
				yield return new WaitForSeconds(BETWEEN_WAVE_DURATION);
				RoundCanvas.SetActive(false);
                StartNewRound();
                SpawnWave();
            }

            float timer = waveDuration;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                if (IsScreenClear())
                {
                    timer = 0;
                    break;
                }
                yield return null;
            }

            SpawnWave();
        }
    }
}
