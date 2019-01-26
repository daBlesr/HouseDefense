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
	private float timerHomeAttack = 2f;

    private Health health;
	[SerializeField] private Image goblinHealthBar;

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
	}

	void takeDamage(int damage)
	{
        health.takeDamage(damage);
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
        Debug.Log("Attacking player");
		isWalking = false;
		StartCoroutine(Movements());
        AttackPlayerEvent?.Invoke(1);
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
        AttackHomeEvent?.Invoke(1);
	}

	IEnumerator RepeatAttack()
	{
		yield return new WaitForSeconds(timerHomeAttack);
		AttackHome();
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

        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(1);
        }
	}
}