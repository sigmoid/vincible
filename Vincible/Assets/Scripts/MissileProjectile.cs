using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
	public GameObject DeathParticles;

	public float Lifetime = 1.0f;

	public float Damage = 1;

	public float Velocity = 10;

	public float RotateSpeed = 0.1f;

	private float _timer;

	// Start is called before the first frame update
	void Start()
	{
		_timer = Lifetime;
		Rotate(true);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		_timer -= Time.deltaTime;

		if (_timer <= 0)
		{
			if (DeathParticles != null)
				Instantiate(DeathParticles, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}

		Rotate(false);

		transform.position += transform.up * Velocity * Time.deltaTime;
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

	private void Rotate(bool snap)
	{
		var player = FindObjectOfType<PlayerController>();

		var diff = player.transform.position - transform.position;
		diff.Normalize();

		var desiredAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		

		var currentAngle = transform.eulerAngles.z;

		desiredAngle -= 90.0f;

		if (!snap)
		{
			transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(currentAngle, desiredAngle, RotateSpeed), Vector3.forward);
		}
		else
		{
			transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
		}
	}
}
