using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

	private HealthSystem health = new HealthSystem();
	[SerializeField] private Image playerHealthImage;


    private Rigidbody2D rigid;
    private Vector2 velocity = new Vector2(0,0);
	private bool isJumping = false;
    private Quaternion rotation = new Quaternion();
    private float jumpVelocity = 20f;
    private const float walkVelocity = 10;
    private const float midAirDrag = 0.7f;
    private int horDirection = 1;
    private Boolean turned = false;

    // Start is called before the first frame update
    void Start()
    {
        // this.gameObject.transform.SetPositionAndRotation(position, rotation);
        rigid = GetComponent<Rigidbody2D>();

		health.MaxHealth = 100;
		health.Health = health.MaxHealth;
		playerHealthImage.fillAmount = (health.Health / 100);
	}

    // Update is called once per frame
    void Update()
    {
        velocity = rigid.velocity;

        Jump();
        Walk();

		rigid.velocity = velocity;
    }

	private void Walk()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        
        if (Math.Abs(horizontalAxis) > 0) {
            
            if (isJumping && (Math.Sign(horizontalAxis) != Math.Sign(horDirection) || turned))
            {
                // mid air movement by turning around, drag is applied.
                rigid.transform.Translate(new Vector2(
                    horizontalAxis * walkVelocity * midAirDrag * Time.deltaTime,
                    0
                ));
                turned = true;
            } else
            {
                rigid.transform.Translate(new Vector2(
                    horizontalAxis * walkVelocity * Time.deltaTime,
                    0
                ));
            }
        }

        horDirection = Math.Sign(horizontalAxis);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            turned = false;
        }
    }

    private void Jump()
	{
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)))
        {
            velocity.y = jumpVelocity;
            isJumping = true;
        }
    }

	void WhenHit(int damage)
	{
		if (health.Health > 0)
		{
			health.Health -= damage;
			playerHealthImage.fillAmount = (health.Health / 100);
		}
	}

	private void OnEnable()
	{
		Goblin.AttackPlayerEvent += WhenHit;
	}

	private void OnDisable()
	{
		Goblin.AttackPlayerEvent -= WhenHit;
	}
}