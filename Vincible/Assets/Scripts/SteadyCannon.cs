using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteadyCannon : MonoBehaviour
{
    public GameObject Projectile;

    public float FireRate;

	private float _fireTimer;

	private CannonUtils _utils;

    // Start is called before the first frame update
    void Start()
    {
        _utils = FindObjectOfType<CannonUtils>();
    }

    // Update is called once per frame
    void Update()
    {
		if (_utils != null && _utils.CanFireCannon(this.transform) != true)
			return;

		if (_fireTimer > 0)
			_fireTimer -= Time.deltaTime;

		if (_fireTimer <= 0)
		{
			SpawnProjectile();
			_fireTimer = 1.0f / FireRate;
		}
	}

	void SpawnProjectile()
	{
		GameObject.Instantiate(Projectile, this.transform.position, this.transform.rotation);
	}
}
