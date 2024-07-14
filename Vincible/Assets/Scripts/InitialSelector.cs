using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InitialSelector : MonoBehaviour
{
	public bool DefaultSelected = false;
	private bool _isSelected = false;

	public InitialSelector Next;
	public List<GameObject> Arrows;
	public TMP_Text Text;

	private bool _joystickDeadLastFrame = true;
	private const float JOYSTICK_DEAD = 0.3f;

	private bool updatedThisFrame = false;
	private int _selectedCharIdx = 0;
	private char[] _availableChars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'/*, '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' */};

	public void Select()
	{
		_isSelected = true;
		foreach (var item in Arrows)
		{
			item.SetActive(true);
		}
		updatedThisFrame = true;
	}

	public string GetChar()
	{
		return _availableChars[_selectedCharIdx].ToString();
	}

	private void Deselect()
	{
		_isSelected = false;
		foreach (var item in Arrows)
		{
			item.SetActive(false);
		}

		if (Next != null)
			Next.Select();
	}

	// Start is called before the first frame update
	void Start()
	{
		foreach (var item in Arrows)
		{
			item.SetActive(false);
		}

		if (DefaultSelected)
			Select();
	}

	// Update is called once per frame
	void Update()
	{
		if (updatedThisFrame)
		{
			updatedThisFrame = false;
			return;
		}

		if (!_isSelected)
			return;

		if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
		{
			Debug.Log("yea");
			Deselect();
		}

		var joystick = Input.GetAxis("Vertical");

		if (joystick < JOYSTICK_DEAD)
		{
			if (_joystickDeadLastFrame)
			{
				_selectedCharIdx++;
				_selectedCharIdx %= _availableChars.Length;
			}
		}
		
		if (joystick > -JOYSTICK_DEAD)
		{
			if (_joystickDeadLastFrame)
			{
				_selectedCharIdx--;
				_selectedCharIdx = (_selectedCharIdx < 0) ? _availableChars.Length - 1 : _selectedCharIdx;
			}
		}

		_joystickDeadLastFrame = joystick < JOYSTICK_DEAD && joystick > -JOYSTICK_DEAD;

		Text.text = _availableChars[_selectedCharIdx].ToString();
	}
}
