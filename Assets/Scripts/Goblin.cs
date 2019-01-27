using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine;

public class Goblin : Character
{
	public static Action<int> DamageEvent;
	public static Action<int> AttackHomeEvent;
	public static Action<int> AttackPlayerEvent;

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject hitBoxEnemy;
    [SerializeField] private Rigidbody2D axe;

	private Animator anim;

	private bool animWalk = false;
	private bool animDead = false;
	private bool animAttack = false;

	private bool isWalking = false;

    private Health health;
    private float previousAttackTime;
    private bool isRanger;
    private float axeVelocity = 50f;
    private float axeThrowDelay = 4f;

	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
        moveSpeed = 1;
		isWalking = true;
        isRanger = UnityEngine.Random.Range(0, 4) == 0;
        health = new Health(5, 3, 2, -4);
    }

    private void OnGUI()
    {
        health.updateHealthBar(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
		Movement();	
        if (health.isDead())
        {
			isWalking = false;
            this.health.destroy();
			anim.SetBool("isDead", true);
            Destroy(this.gameObject,2);
        }

        if (isRanger && Time.time - previousAttackTime >= axeThrowDelay)
        {
            ShootAtPlayer();
            previousAttackTime = Time.time;
        }
	}

	void takeDamage(int damage)
	{
        health.takeDamage(damage);
	}

	private void Movement()
	{
		if(isWalking == true)
		{
			anim.SetBool("isWalking", true);
			this.gameObject.transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
			Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
		}
	}

    private void ShootAtPlayer()
    {
        Rigidbody2D newAxe = Instantiate(axe, transform.position, transform.rotation);
        float randomRad = Mathf.Deg2Rad * UnityEngine.Random.Range(0, 70);
        newAxe.velocity = new Vector2(
              - Mathf.Cos(randomRad) * axeVelocity,
              Mathf.Sin(randomRad) * axeVelocity
        );
    }

	private void AttackPlayer()
	{
		AttackPlayerEvent?.Invoke(1);
    }

	private void AttackHome()
	{
		AttackHomeEvent?.Invoke(1);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player")
		{
            isWalking = false;
			if (Time.time - previousAttackTime >= 1)
            {
                AttackPlayer();
				previousAttackTime = Time.time;
            }
            return;
        }

		if (collision.gameObject.tag == "Home")
		{
            isWalking = false;
			if (Time.time - previousAttackTime >= 1)
            {
                AttackHome();
				previousAttackTime = Time.time;
            }
            return;
        }

		isWalking = true;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Axe")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
        }
    }
}