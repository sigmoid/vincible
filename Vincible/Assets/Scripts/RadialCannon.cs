using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialCannon : MonoBehaviour
{
    public GameObject Projectile;

	public float NumProjectiles;

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
		for (int i = 0; i < NumProjectiles; i++)
		{
			var rotation = Quaternion.AngleAxis(90.0f + (float)i * (180.0f/(float)(NumProjectiles - 1)), Vector3.forward);
			GameObject.Instantiate(Projectile, this.transform.position, rotation);
		}
	}
}
