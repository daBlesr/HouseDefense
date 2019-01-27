using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Health health;

	void Start()
    {
		health = new Health(10, 5, 3, 0);
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
