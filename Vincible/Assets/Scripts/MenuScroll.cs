using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScroll : MonoBehaviour
{
    public GameObject[] Items;

    public float ItemDuration = 3;

    private int _currentItem = 0;

    private float _timer;

    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        _timer = ItemDuration;
        SetCurrentIdx(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                AdvanceItem();
            }
        }

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
            AdvanceItem();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    void AdvanceItem()
    {
		_currentItem++;
		_currentItem %= Items.Length;
		SetCurrentIdx(_currentItem);
		_timer = ItemDuration;
	}

    void SetCurrentIdx(int idx)
	{
		for (int i = 0; i < Items.Length; i++)
		{
            Items[i].gameObject.SetActive(false);
            if(i == idx)
                Items[i].gameObject.SetActive(true);
		}
	}
}
