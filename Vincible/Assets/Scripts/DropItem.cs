using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject Item;
    public float DropChance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void TrySpawn()
	{
        var rand = Random.Range(0.0f, 1.0f);

        if (rand < DropChance)
            Instantiate(Item, transform.position, Quaternion.identity);
	}
}
