using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject DeathObject;

    public int StartHealth;

    public int ScoreValue = 0;

    public float FlashDuration = 0.05f;

    private int _health;

    private float _flashTimer;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _health = StartHealth;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_flashTimer > 0)
        {
            _flashTimer -= Time.deltaTime;

            _spriteRenderer.color = new Color(1, 1, 1, 0.2f);

            if (_flashTimer <= 0)
            {
                _spriteRenderer.color = Color.white;
            }
        }

        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
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
        else
        {
            _flashTimer = FlashDuration;

		}
    }
}