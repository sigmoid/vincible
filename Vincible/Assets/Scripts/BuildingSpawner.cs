using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject Building;

    public List<Transform> Spawns;

    public float Spawnrate;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 1.0f / Spawnrate;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;

            if (_timer < 0)
            {
                SpawnBuildings();

                _timer = 1.0f / Spawnrate;
            }
        }
    }

    public void SpawnBuildings()
    { 
        foreach(var t in Spawns)
        {
            GameObject.Instantiate(Building, t.position, t.rotation);
        }
    }
}
