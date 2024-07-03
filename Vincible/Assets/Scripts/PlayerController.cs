using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float LateralSpeed = 10;
    public float VerticalSpeed = 10;

    public float MaxX, MaxY;

    public Sprite BankRightSprite, BankLeftSprite;
    public Sprite[] DefaultAnimationSprites;
    public float DefaultAnimationFramerate;

    private int _currentAnimationFrame;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private const float VELOCITY_DEAD_ZONE = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (transform.position.x > MaxX && moveInput.x > 0)
            moveInput.x = 0;
		if (transform.position.x < -MaxX && moveInput.x < 0)
			moveInput.x = 0;
		if (transform.position.y > MaxY && moveInput.y > 0)
			moveInput.y = 0;
		if (transform.position.y < -MaxY && moveInput.y < 0)
			moveInput.y = 0;


		moveInput.Normalize();

        var moveAmount = new Vector3(moveInput.x * LateralSpeed, moveInput.y * VerticalSpeed, 0);

        _rigidbody.velocity = moveAmount;

        UpdateSprite();
    }

    void UpdateSprite()
    {
        float frameDuration = 1.0f / DefaultAnimationFramerate;
        _currentAnimationFrame = Mathf.RoundToInt(Time.time / frameDuration) % DefaultAnimationSprites.Length;
        if (_rigidbody.velocity.x < -VELOCITY_DEAD_ZONE)
            _spriteRenderer.sprite = BankLeftSprite;
        else if (_rigidbody.velocity.x > VELOCITY_DEAD_ZONE)
            _spriteRenderer.sprite = BankRightSprite;
        else
        {

            _spriteRenderer.sprite = DefaultAnimationSprites[_currentAnimationFrame];
        }
    }

}
