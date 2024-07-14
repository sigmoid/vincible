using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float LateralSpeed = 18;
    public float VerticalSpeed = 18;

    public float MaxX, MaxY;

    public float FireMoveModifier = 0.5f;

    public Sprite BankRightSprite, BankLeftSprite;
    public Sprite[] DefaultAnimationSprites;
    public float DefaultAnimationFramerate;

    private int _currentAnimationFrame;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private const float VELOCITY_DEAD_ZONE = 0.2f;

    private bool _inputFrozen = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    public void FreezeInput()
    {
        _inputFrozen = true;
    }

	private void Update()
	{
        if(_inputFrozen)
            return;
		var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if (Input.GetButtonDown("Fire2"))
		{
			FindObjectOfType<PowerupManager>().ConsumePowerup();
		}

        UpdateSprite(moveInput);
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (_inputFrozen)
            moveInput = Vector2.zero;

        if (transform.position.x > MaxX && moveInput.x > 0)
            moveInput.x = 0;
		if (transform.position.x < -MaxX && moveInput.x < 0)
			moveInput.x = 0;
		if (transform.position.y > MaxY && moveInput.y > 0)
			moveInput.y = 0;
		if (transform.position.y < -MaxY && moveInput.y < 0)
			moveInput.y = 0;

		moveInput.Normalize();

        var modifier = 1.0f;

        if (Input.GetButton("Fire1") && !_inputFrozen)
        {
            modifier *= FireMoveModifier;
        }

        var moveAmount = new Vector3(moveInput.x * LateralSpeed * modifier, moveInput.y * VerticalSpeed * modifier, 0);

        _rigidbody.MovePosition(transform.position + moveAmount * Time.fixedUnscaledDeltaTime);
    }

    void UpdateSprite(Vector2 moveInput)
    {
        float frameDuration = 1.0f / DefaultAnimationFramerate;
        _currentAnimationFrame = Mathf.RoundToInt(Time.unscaledTime / frameDuration) % DefaultAnimationSprites.Length;
        if (moveInput.x < -VELOCITY_DEAD_ZONE)
            _spriteRenderer.sprite = BankLeftSprite;
        else if (moveInput.x > VELOCITY_DEAD_ZONE)
            _spriteRenderer.sprite = BankRightSprite;
        else
        {

            _spriteRenderer.sprite = DefaultAnimationSprites[_currentAnimationFrame];
        }
    }

}
