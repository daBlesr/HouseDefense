using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Vector2 velocity = new Vector2(0,0);
	private bool isJumping = false;
    [SerializeField] private float jumpVelocity = 20f;
    private const float walkVelocity = 10;
    private const float midAirDrag = 0.7f;
    private int horDirection = 1;
    private Boolean turned = false;

	private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        velocity = rigid.velocity;

		Idle();
		Jump();
        Walk();

		rigid.velocity = velocity;
    }

	private void Idle()
	{
		float horizontalAxis = Input.GetAxis("Horizontal");

		if (Math.Abs(horizontalAxis) <= 0)
		{
			anim.SetBool("isWalking", false);
		}
	}

	private void Walk()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        int lookDirection = gameObject.transform.rotation.eulerAngles.y == 180 ? -1 : 1;


        if (Math.Abs(horizontalAxis) > 0) {
            
            if (isJumping && (Math.Sign(horizontalAxis) != Math.Sign(horDirection) || turned))
            {
                // mid air movement by turning around, drag is applied.
                rigid.transform.Translate(new Vector2(
                    lookDirection * horizontalAxis * walkVelocity * midAirDrag * Time.deltaTime,
                    0
                ));
                turned = true;
            } else
            {
                rigid.transform.Translate(new Vector2(
                    lookDirection * horizontalAxis * walkVelocity * Time.deltaTime,
                    0
                ));
				anim.SetBool("isWalking", true);
            }
        }
        horDirection = Math.Sign(horizontalAxis);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
			anim.SetBool("isJumping", false);
            isJumping = false;
            turned = false;
        }
    }

    private void Jump()
	{
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            velocity.y = jumpVelocity;
            isJumping = true;
			anim.SetBool("isJumping", true);
			anim.SetBool("isWalking", false);
		}
    }
}