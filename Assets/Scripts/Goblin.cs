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

	private bool isWalking = false;

    private Health health;
	[SerializeField] private Image goblinHealthBar;
    private float previousAttackTime;

	// Start is called before the first frame update
	void Start()
    {
		moveSpeed += 1 * Time.deltaTime;
		isWalking = true;

        health = new Health(3, goblinHealthBar);
    }

    // Update is called once per frame
    void Update()
    {
		Movement();	
        if (health.isDead())
        {
            Destroy(this.gameObject);
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
            Debug.Log("iswalking");
			this.gameObject.transform.position += new Vector3(-moveSpeed, 0, 0);
			Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
		}
	}

	private void AttackPlayer()
	{
        Debug.Log("isattacking");
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
            Debug.Log("Faced Player");
            isWalking = false;
            Debug.Log(Time.time - previousAttackTime);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(1);
        }
    }
}