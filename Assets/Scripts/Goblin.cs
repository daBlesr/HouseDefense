using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Character
{
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject hitBoxAttack;

	private bool isWalking = false;
	private bool isAttacking = false;

	// Start is called before the first frame update
	void Start()
    {
		moveSpeed += 1 * Time.deltaTime;
		isWalking = true;
		isAttacking = false;
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
			Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}

	private void Attack(bool isAttack)
	{
		isAttacking = isAttack;
		if(isAttacking == true)
		{
			Debug.Log("Deal Damage");
			isWalking = false;
		}
		else
		{
			Debug.Log("isWalking");
			isWalking = true;
		}
	}

	public override void TakeDamage()
	{
		//If Hit, TakeDamage
		base.TakeDamage();
	}

	private void OnEnable()
	{
		HitBoxTrigger.HitEvent += Attack;
	}

	private void OnDisable()
	{
		HitBoxTrigger.HitEvent -= Attack;
	}
}