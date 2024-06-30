using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstCannon : MonoBehaviour
{
    public GameObject Projectile;

	public float BurstRate;
	public float BurstDuration;

    public float FireRate;

	private float _fireTimer;
	private float _burstTimer;
	private float _burstDurationTimer;

    // Start is called before the first frame update
    void Start()
    {
		_burstTimer = 1.0f / BurstRate;
    }

    // Update is called once per frame
    void Update()
    {
		if (_burstTimer > 0)
		{
			_burstTimer -= Time.deltaTime;

			if (_burstTimer <= 0)
				_burstDurationTimer = BurstDuration;
		}
		else
		{
			if (_burstDurationTimer > 0)
				_burstDurationTimer -= Time.deltaTime;
			else
				_burstTimer = 1.0f / BurstRate;

			if (_fireTimer > 0)
				_fireTimer -= Time.deltaTime;

			if (_fireTimer <= 0)
			{
				SpawnProjectile();
				_fireTimer = 1.0f / FireRate;
			}
		}
	}

	void SpawnProjectile()
	{
		GameObject.Instantiate(Projectile, this.transform.position, this.transform.rotation);
	}
}
