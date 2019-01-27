using System.Collections;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
	public static Action playerDeadEvent;

    private Health health;
	private Animator anim; 

	void Start()
    {
		anim = GetComponent<Animator>();
		health = new Health(10, 5, 8, 0);
	}

	private void Update()
	{
		if (health.isDead())
		{
			this.health.destroy();
			anim.SetBool("isDead", true);
			Destroy(this.gameObject, 4);
			if(playerDeadEvent != null)
			{
				playerDeadEvent();
			}
		}
	}

	private void OnGUI()
    {
        health.updateHealthBar(transform.position);
	}

    void takeDamage(int damage)
    {
		StartCoroutine(Delay());
        health.takeDamage(damage);
    }

	IEnumerator Delay()
	{
		yield return new WaitForSeconds(2f);
	}

    private void OnEnable()
    {
        Goblin.AttackPlayerEvent += takeDamage;
    }

    private void OnDisable()
    {
        Goblin.AttackPlayerEvent -= takeDamage;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Axe")
        {
            takeDamage(1);
        }
    }
}
