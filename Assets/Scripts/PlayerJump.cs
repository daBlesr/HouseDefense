using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

	private Vector2 position = new Vector2(0,0);
	private bool isJumping = false;
    private bool isFalling = false;
	private float maxHeight = 10;
    private Quaternion rotation = new Quaternion();
    private const float jumpVelocity = 20;
    private const float fallVelocity = 30;
    private const float walkVelocity = 10;
    private const float midAirDrag = 0.5f;
    private Vector2 speedVector = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
		this.gameObject.transform.SetPositionAndRotation(position, rotation);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

        Jump();
        Walk();

        speedVector = position - currentPosition;
        this.gameObject.transform.SetPositionAndRotation(position, rotation);
    }

    private void Walk()
    {
        // float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        
        if (Math.Abs(horizontalAxis) > 0) {
            if (isJumping && Math.Sign(horizontalAxis) != Math.Sign(speedVector.x))
            {
                // mid air movement by turning around, drag is applied.
                position.x += horizontalAxis * walkVelocity * Time.deltaTime * midAirDrag;
            } else
            {
                position.x += horizontalAxis * walkVelocity * Time.deltaTime;
            }
        }
    }

    private void Jump()
	{
        if (Input.GetButtonDown("Fire1") && !isJumping)
        {
			isJumping = true;
		}

        if (isJumping)
        {
            if (position.y < maxHeight && !isFalling)
            {
                position.y += jumpVelocity * Time.deltaTime;
            }
            else
            {
                isFalling = true;
                position.y -= fallVelocity * Time.deltaTime;

                if (position.y <= 0)
                {
                    position.y = 0;
                    isJumping = false;
                    isFalling = false;
                }
            }
        }
    }
}