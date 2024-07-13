using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject DeathParticles;

    public float Lifetime = 1.0f;

    public float Damage = 1;

    public float Velocity = 10;

    private float _timer;

    public bool UseScaledTime = true;

    // Start is called before the first frame update
    void Start()
    {
        _timer = Lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = (UseScaledTime) ? Time.deltaTime : Time.unscaledDeltaTime;
     
        _timer -= delta;

        if (_timer <= 0)
            Destroy(this.gameObject);

        transform.position += transform.up * Velocity * delta;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{

        if (!collision.gameObject.CompareTag("BulletKill"))
        {
            collision.gameObject.SendMessage("Hit", Damage, SendMessageOptions.DontRequireReceiver);

            if (DeathParticles != null)
                Instantiate(DeathParticles, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
	}
}
