using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject DeathObject;

    public int StartHealth;

    public int ScoreValue = 0;

    private int _health;

    // Start is called before the first frame update
    void Start()
    {
        _health = StartHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            if (DeathObject != null)
            {
                Instantiate(DeathObject, transform.position, transform.rotation);
            }

            var scoreManager = FindObjectOfType<ScoreManager>();

            if (scoreManager != null)
            {
                scoreManager.AddScore(ScoreValue);
            }

            var dropItem = GetComponent<DropItem>();

            if (dropItem != null)
            {
                dropItem.TrySpawn();
            }

            Destroy(this.gameObject);
        }
    }
}