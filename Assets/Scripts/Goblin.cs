using System;
using System.Collections;
using UnityEngine;

public class Goblin : Character
{
	public static Action<int> CoinUpEvent;

	[SerializeField] private int coinValue = 5;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject hitBoxEnemy;

	private bool isWalking = false;
	private float timerHomeAttack = 2f;


	// Start is called before the first frame update
	void Start()
    {
		moveSpeed += 1 * Time.deltaTime;
		isWalking = true;
		TakeDamage();
	}

    // Update is called once per frame
    void Update()
    {
		Movement();	
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
		base.TakeDamage();

		health -= 1000; //Test for coins

		if(health <= 0)
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