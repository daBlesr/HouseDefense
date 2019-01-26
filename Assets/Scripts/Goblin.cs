using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine;

public class Goblin : Character
{
	public static Action<int> CoinUpEvent;
	public static Action<int> DamageEvent;
	public static Action<int> AttackHomeEvent;
	public static Action<int> AttackPlayerEvent;

	[SerializeField] private int coinValue = 5;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject hitBoxEnemy;

	private bool isWalking = false;
	private float timerHomeAttack = 2f;

	private HealthSystem health = new HealthSystem();
	[SerializeField] private Image goblinHealthBar;

	// Start is called before the first frame update
	void Start()
    {
		moveSpeed += 1 * Time.deltaTime;
		isWalking = true;

		health.MaxHealth = 100;
		health.Health = health.MaxHealth;
		goblinHealthBar.fillAmount = (health.Health / 100);
	}

    // Update is called once per frame
    void Update()
    {
		Movement();	
	}


	void WhenHit(int damage)
	{
		if (health.Health > 0)
		{
			health.Health -= damage;
			goblinHealthBar.fillAmount = (health.Health / 100);
		}
	}

	private void Movement()
	{
		if(isWalking == true)
		{
			this.gameObject.transform.position += new Vector3(-moveSpeed, 0, 0);
			Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
		}
	}

	private void AttackPlayer()
	{
		Debug.Log("Deal Damage");
		isWalking = false;
		StartCoroutine(Movements());
		if(AttackPlayerEvent != null)
		{
			AttackPlayerEvent(20);
		}
	}

	IEnumerator Movements()
	{
		yield return new WaitForSeconds(1);
		isWalking = true;
	}

	private void AttackHome()
	{
		Debug.Log("Deal Massive Home Damage");
		isWalking = false;

		if (AttackHomeEvent != null)
		{
			AttackHomeEvent(10);
		}
		StartCoroutine(RepeatAttack());
	}

	IEnumerator RepeatAttack()
	{
		yield return new WaitForSeconds(timerHomeAttack);
		AttackHome();
	}

	public override void TakeDamage()
	{
		//If Hit, TakeDamage

		if(DamageEvent != null)
		{
			DamageEvent(10);
		}



		if(100 <= 0)
		{
			//Play Dead Animation
			if (CoinUpEvent != null)
			{
				CoinUpEvent(coinValue);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			AttackPlayer();
		}

		if (collision.gameObject.tag == "Home")
		{
			AttackHome();
		}
	}
}