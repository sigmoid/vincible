using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject DeathObject;

    public int StartHealth;

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
            Destroy(this.gameObject);
        }
    }
}