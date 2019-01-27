using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	[SerializeField] private GameObject secondLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
        {
            Destroy(gameObject);
        } else
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
		}
	}
}
