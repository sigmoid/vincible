using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BasicCannon : MonoBehaviour
{
    public GameObject Projectile;
    public float FireRate;

    private bool _inputFrozen;

    private float _fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputFrozen)
            return;

        if(_fireTimer > 0)
            _fireTimer -= Time.unscaledDeltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (_fireTimer <= 0)
            {
                SpawnProjectile();
                _fireTimer = 1.0f / FireRate;
            }
        }

    }

    public void FreezeInput()
    {
        _inputFrozen = true;
    }

    void SpawnProjectile()
    {
        var projectile = GameObject.Instantiate(Projectile, this.transform.position,this.transform.rotation);
    }
}
