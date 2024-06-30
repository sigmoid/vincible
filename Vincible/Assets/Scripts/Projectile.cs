using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Lifetime = 1.0f;

    public float Damage = 1;

    public float Velocity = 10;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = Lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
            Destroy(this.gameObject);

        transform.position += transform.up * Velocity * Time.deltaTime;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        collision.gameObject.SendMessage("Hit", Damage, SendMessageOptions.DontRequireReceiver);

        Destroy(this.gameObject);
	}
}